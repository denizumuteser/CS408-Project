using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CS408Project_Server
{

    public partial class Form1 : Form
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        public List<string> clientList = new List<string>();

        public IDictionary<Socket, string> clientDict = new Dictionary<Socket, string>();

        bool terminating = false;
        bool listening = false;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            Console.WriteLine("Started");
            //removeFriend("lili", "deniz");
            //addFriend("lili", "mirac");
            InitializeComponent();
        }

        private void button_Listen_Click(object sender, EventArgs e)
        {
            string port = textBox_Port.Text;
            int portNum;
            if (!Int32.TryParse(port, out portNum))
            {//port is not a number
                richTextBox_Console.AppendText("Check the port number!\n");
                return;
            }
            //start listening
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, portNum);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(3);
            richTextBox_Console.AppendText("Started listening on port: " + port + ".\n");

            listening = true;
            button_Listen.Enabled = false;

            Thread acceptThread = new Thread(Accept);
            acceptThread.Start();
        }

        private void Accept()
        {
            while (listening)
            {
                try
                {//listening
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);
                    Thread receiveThread = new Thread(() => Receive(newClient));
                    receiveThread.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        richTextBox_Console.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void Receive(Socket thisClient)
        {
            bool connected = true;
            //logged.clientList.Add("lili");

            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[4096]; //byte array
                    thisClient.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf('\0'));
                    Console.WriteLine(incomingMessage);
                    String[] messages = incomingMessage.Split('|');
                    //classify coming message according to header
                    Byte[] logtoclient;

                    if (messages[0] == "CONNECT_REQUEST")
                    {
                        string username = messages[1];
                        
                        //if user is already registered
                        if (isUsernameExistDatabase(username) && !isUsernameExistCurrently(username))
                        {
                            //connected
                            //send message to server console
                            richTextBox_Console.AppendText(username + " has connected.\n");
                            //send message to client
                            logtoclient = Encoding.Default.GetBytes("1|Hello " + username + "! You are connected to the server.\n");
                            thisClient.Send(logtoclient);
                            clientDict.Add(thisClient, username);
                            //sending friends
                            string friendlist = getFriends(username);
                            if (friendlist != "")
                            {
                                //send friends to client
                                logtoclient = Encoding.Default.GetBytes("4$" + friendlist);
                                thisClient.Send(logtoclient);
                            }
                        }
                        else if (isUsernameExistCurrently(username))
                        {
                            richTextBox_Console.AppendText(username + " has already connected! \n");
                            logtoclient = Encoding.Default.GetBytes("0|" + username + " has already connected! \n");
                            thisClient.Send(logtoclient);
                        }
                        else
                        {
                            //cannot connect
                            //send message to server console
                            richTextBox_Console.AppendText(username + " tried to connect to the server but cannot!.\n");
                            //send message to client
                            logtoclient = Encoding.Default.GetBytes("0|Please enter a valid username\n");
                            thisClient.Send(logtoclient);
                        }

                    }
                    else if (messages[0] == "POST")
                    {
                        string username = messages[1];
                        string post = messages[2];
                        string timestamp = messages[3];
                        //handle post
                        handlePost(username, post, timestamp);
                    }
                    else if (messages[0] == "RETRIEVE_ALL")
                    {
                        //print to server console
                        string username = messages[1];
                        richTextBox_Console.AppendText("Showed all posts for " + username + ".\n");
                        //handle post retriever
                        postRetriever(username, thisClient,2);
                    }
                    else if (messages[0] == "RETRIEVE_FRIENDSPOST")
                    {
                        //print to server console
                        string username = messages[1];
                        richTextBox_Console.AppendText("Showed friends posts for " + username + ".\n");
                        //handle post retriever
                        postRetriever(username, thisClient,6);
                    }
                    else if (messages[0] == "RETRIEVE_MYPOST")
                    {
                        //print to server console
                        string username = messages[1];
                        richTextBox_Console.AppendText("Showed their posts for " + username + ".\n");
                        //handle post retriever
                        postRetriever(username, thisClient, 7);
                    }

                    else if (messages[0] == "DISCONNECTED")
                    {
                        string username = messages[1];
                        clientList.Remove(username);

                        richTextBox_Console.AppendText(messages[1] + " has disconnected.\n");
                    }
                    else if (messages[0] == "ADDFRIEND")
                    {
                        string username = messages[1];
                        string friend = messages[2];
                        
                        if (username == friend)
                        {//failed
                            richTextBox_Console.AppendText("You cannot send a request to yourself\n");
                            logtoclient = Encoding.Default.GetBytes("3|You cannot send a request to yourself\n");
                            thisClient.Send(logtoclient);
                        }
                        else
                        {
                            addFriend(username, friend);
                            //sent to user
                            logtoclient = Encoding.Default.GetBytes("3|You have added " + friend + " as a friend\n");
                            thisClient.Send(logtoclient);
                            //update friend list
                            string friendlist = getFriends(username);
                            if (friendlist != "")
                            {
                                //send friends to client
                                logtoclient = Encoding.Default.GetBytes("4$" + friendlist);
                                thisClient.Send(logtoclient);
                            }
                            //sent to users friend
                            foreach (var client in clientDict)
                            {

                                if (client.Value == friend)
                                {
                                    logtoclient = Encoding.Default.GetBytes("3|" + username + " added you as a friend. You are friend now.\n");
                                    client.Key.Send(logtoclient);
                                    //update friend list
                                    friendlist = getFriends(username);
                                    if (friendlist != "")
                                    {
                                        //send friends to client
                                        logtoclient = Encoding.Default.GetBytes("4$" + friendlist);
                                        thisClient.Send(logtoclient);
                                    }
                                }
                            }
                        }
                    }
                    else if (messages[0] == "REMOVEFRIEND")
                    {
                        string username = messages[1];
                        string friend = messages[2];
                        Byte[] logtoclient2;
                        if (username == friend)
                        {//failed
                            richTextBox_Console.AppendText("You cannot send a request to yourself");
                            logtoclient2 = Encoding.Default.GetBytes("3|You cannot send a request to yourself");
                            thisClient.Send(logtoclient2);
                        }
                        removeFriend(username, friend);
                        //sent to user
                        logtoclient2 = Encoding.Default.GetBytes("3|You have removed " + friend + " from friends");
                        thisClient.Send(logtoclient2);
                        //update friend list
                        string friendlist = getFriends(username);
                        if (friendlist != "")
                        {
                            //send friends to client
                            logtoclient = Encoding.Default.GetBytes("4$" + friendlist);
                            thisClient.Send(logtoclient);
                        }
                        else //no friend left
                        {
                            logtoclient = Encoding.Default.GetBytes("5$");
                            thisClient.Send(logtoclient);
                        }
                        //sent to users friend
                        foreach (var client in clientDict)
                        {

                            if (client.Value == friend)
                            {
                                logtoclient2 = Encoding.Default.GetBytes("3|" + username + " removed you from friends.");
                                client.Key.Send(logtoclient2);
                                //update friend list
                                friendlist = getFriends(username);
                                if (friendlist != "")
                                {
                                    //send friends to client
                                    logtoclient = Encoding.Default.GetBytes("4$" + friendlist);
                                    thisClient.Send(logtoclient);
                                }
                            }
                        }

                    }

                    else if (messages[0] == "REQUESTFRIENDS")
                    {
                        string username = messages[1];
                        string friendlist = getFriends(username);
                        if (friendlist!="")
                        {
                            //send friends to client
                            logtoclient = Encoding.Default.GetBytes("4$"+friendlist);
                            thisClient.Send(logtoclient);
                        }
                    }

                    else if (messages[0] == "DELETEPOST")
                    {
                        string username = messages[1];
                        Int32.TryParse(messages[2], out int toBeDeletedID);
                        int status = deletePost(username, toBeDeletedID);
                        string messageToSend = "";
                        if (status == 1)//deleted
                        {
                            //server log
                            richTextBox_Console.AppendText("3|Post with id: " + toBeDeletedID + " is deleted successfully.\n");
                            messageToSend = "Post with id: " + toBeDeletedID + " is deleted successfully.\n";

                        }
                        else if (status == 2)//post is not yours
                        {
                            richTextBox_Console.AppendText("3|Post with id: " + toBeDeletedID + "is not " + username + "s!\n");
                            messageToSend = "Post with id: " + toBeDeletedID + "is not yours!";
                        }
                        else if (status == 3)//no post with such id
                        {
                            richTextBox_Console.AppendText("3|Post with id: " + toBeDeletedID + " does not exist!\n");
                            messageToSend = "There is no posts with id: " + toBeDeletedID;

                        }
                        if (messageToSend != "")
                        {
                            logtoclient = Encoding.Default.GetBytes(messageToSend);
                            thisClient.Send(logtoclient);
                        }


                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        //richTextBox_Console.AppendText("Client has disconnected.\n");
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = false;
                }
            }
        }

        private string getFriends(string username)
        {
            var lines = File.ReadLines(@"../../friends.txt");
            foreach (var line in lines)
            {
                string[] words = line.Split('$');
                if (username == words[0])
                {
                    return words[1];
                }
            }
            //not found username in database
            return "";
        }

        private void postRetriever(string username, Socket thisclient, int posttype) //2 retrieve all //6 retrieve friends posts //7 retrieve users posts
        {
            Console.WriteLine("1");
            string all_posts = "";
            //get posts from log
            var lines = File.ReadLines(@"../../posts.log");
            foreach (var line in lines)
            {
                all_posts += line+"\n";
            }
            //send posts to client
            Byte[] logtoclient = Encoding.Default.GetBytes(posttype+"$"+all_posts);
            thisclient.Send(logtoclient);
        }

        private void handlePost(string username, string post, string timestamp)
        {
            //print server console
            richTextBox_Console.AppendText(username + " has sent a post:\n");
            richTextBox_Console.AppendText(post+"\n");
            //add post to posts.log
            if (!File.Exists(@"../../posts.log"))
            {
                Console.WriteLine("no path");
            }
            int id = getLastPostId() + 1;
            using (StreamWriter w = File.AppendText(@"../../posts.log"))
            {
                w.WriteLine(username+"|"+id+"|"+post+"|"+timestamp);
            }
        }

        private int deletePost(string username, int postid)
        {
            var lineFile = File.ReadAllLines(@"../../posts.log");
            List<string> lines = new List<string>(lineFile);

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
           
                int lineid = 0;
                string[] currentline = line.Split('|');
                string usernameOnLine = currentline[0];
                Int32.TryParse(currentline[1], out lineid);
                if (lineid == postid && username == usernameOnLine)
                {
                    //delete post
                    lines.RemoveAt(i);
                    //close file
                    File.WriteAllLines(@"../../posts.log", lines.ToArray());
                    return 1;
                }
            }
            //failed
            return -1;
        }

        private int getLastPostId()
        {
            Console.WriteLine("2");
            int lastid = 0;
            var lines = File.ReadLines(@"../../posts.log");
            foreach (var line in lines)
            {
                int lineid = 0; 
                Int32.TryParse((line.Split('|')[1]), out lineid);
                if (lineid > lastid)
                {
                    lastid = lineid;
                }
            }
            return lastid;
        }

        private int removeFriend(string username, string friendname)
        {
            if (!File.Exists(@"../../friends.txt"))
            {
                Console.WriteLine("no path");
            }
            var lineFile = File.ReadAllLines(@"../../friends.txt");
            List<string> lines = new List<string>(lineFile);

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] words = line.Split('$');
                string usernameOnLine = words[0];

                if (usernameOnLine == username)
                {
                    string[] userfriends = words[1].Split('|');
                    var userfriendslist = userfriends.ToList();
                    userfriendslist.Remove(friendname);
                    //no friends
                    if (userfriendslist.Count() < 1)
                    {
                        lines.Remove(lines[i]);
                        File.WriteAllLines(@"../../friends.txt", lines.ToArray());
                        return 1;
                    }
                    else
                    {
                        string newFriendsLine = String.Join("|", userfriendslist.ToArray());

                        lines[i] = usernameOnLine + "$" + newFriendsLine;
                        File.WriteAllLines(@"../../friends.txt", lines.ToArray());
                        return 1;
                    }
                        
                    
                }
            }
            return 1;
        }

        private int addFriend(string username, string friendname)
        {
            if (!File.Exists(@"../../friends.txt"))
            {
                Console.WriteLine("no path");
            }            
            var lineFile = File.ReadAllLines(@"../../friends.txt");
            List<string> lines = new List<string>(lineFile);

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

                string[] currentline = line.Split('$');
                string usernameOnLine = currentline[0];
                string[] userfriends = currentline[1].Split('|');
                
                if (username == usernameOnLine)
                {//user already exists
                 //on users line

                    foreach (var friends in userfriends)
                    {
                        if (friendname == friends)
                        {
                            return 1; //already friended
                        }
                    }

                    Array.Resize(ref userfriends, userfriends.Length + 1);
                    //if friend already exists
                    userfriends[userfriends.Length - 1] = friendname;
                    string newFriendsLine = String.Join("|", userfriends.ToArray());
                    lines[i] = usernameOnLine + "$" + newFriendsLine;
                    Console.WriteLine(lines[i]);
                    File.WriteAllLines(@"../../friends.txt", lines.ToArray());
                    return 1;
                }
            }
            //user not on list
            //add new line
            Console.WriteLine(1);
            using (StreamWriter w = File.AppendText(@"../../friends.txt"))
            {
                w.WriteLine(username+"$"+friendname);
            }
            return 1;
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {// Avoid crash at closing
            listening = false;
            terminating = true;

            foreach (var socket in clientSockets)
            {
                Byte[] logtoclient = Encoding.Default.GetBytes("3|Server has disconnected!");
                socket.Send(logtoclient); //
            }
            Environment.Exit(0); //release environment resources
        }

        public bool isUsernameExistCurrently(string username)
        {
            bool isExists = false;
            foreach (var item in clientList)
            {
                if (item == username)
                {
                    return true;
                }
            }
            clientList.Add(username);
            return false;
        
        }

        public bool isUsernameExistDatabase(string username)
        {             
            var lines = File.ReadLines(@"../../user-db.txt");
            foreach (var line in lines)
            {
                if (username == line)
                {//found matching username in database
                    return true;
                }
            }
            //not found username in database
            return false;
        }
    }
}
