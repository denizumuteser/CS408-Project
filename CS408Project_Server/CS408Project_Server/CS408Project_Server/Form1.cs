using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS408Project_Server
{
    public partial class Form1 : Form
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();

        bool terminating = false;
        bool listening = false;
        bool tried = false;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            Console.WriteLine("Started");
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

            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[64]; //byte array
                    thisClient.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf('\0'));
                    Console.WriteLine(incomingMessage);
                    String[] messages = incomingMessage.Split('|');
                    //classify coming message according to header
                    if (messages[0] == "CONNECT_REQUEST")
                    {
                        string username = messages[1];

                        //if user is already registered
                        if (isUsernameExist(username))
                        {
                            //connected
                            //send message to server console
                            richTextBox_Console.AppendText(username + " has connected.\n");
                            //send message to client
                            Byte[] logtoclient = Encoding.Default.GetBytes("1|Hello " + username + "! You are connected to the server.\n");
                            thisClient.Send(logtoclient);  
                        }
                        else
                        {
                            //cannot connect
                            //send message to server console
                            tried = true;
                            richTextBox_Console.AppendText(username + " tried to connect to the server but cannot!.\n");
                            //send message to client
                            Byte[] logtoclient = Encoding.Default.GetBytes("0|Please enter a valid username\n");
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
                    else if(messages[0] == "RETRIEVE_ALL")
                    {
                        //print to server console
                        string username = messages[1];
                        richTextBox_Console.AppendText("Showed all posts for "+username+".\n");
                        //handle post retriever
                        postRetriever(username, thisClient);
                    }
                }
                catch
                {
                    if (!terminating && !tried)
                    {
                        richTextBox_Console.AppendText("Client has disconnected.\n");
                    }
                    if (tried)
                    {
                        tried = false;
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = false;
                }
            }
        }

        private void postRetriever(string username, Socket thisclient)
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
            Byte[] logtoclient = Encoding.Default.GetBytes("2$"+all_posts);
            thisclient.Send(logtoclient);
        }

        private void handlePost(string username, string post, string timestamp)
        {
            //print server console
            richTextBox_Console.AppendText(username + "has sent a post:\n");
            richTextBox_Console.AppendText(post+"\n");
            //add post to posts.log
            using (StreamWriter w = File.AppendText("posts.log"))
            {
                w.WriteLine(username+"|"+(getLastPostId()+1)+post+"|"+timestamp);
            }
        }

        private int getLastPostId()
        {
            Console.WriteLine("2");
            int lastid = 0;
            var lines = File.ReadLines(@"../../posts.log");
            foreach (var line in lines)
            {
                lastid += 1;
            }
            return lastid;
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {// Avoid crash at closing
            listening = false;
            terminating = true;

            foreach (var socket in clientSockets)
            {
                Byte[] logtoclient = Encoding.Default.GetBytes("3|Server has disconnected!");
                socket.Send(logtoclient);
            }
            Environment.Exit(0); //release environment resources
        }

        private bool isUsernameExist(string username)
        {
            Console.WriteLine("3");
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
