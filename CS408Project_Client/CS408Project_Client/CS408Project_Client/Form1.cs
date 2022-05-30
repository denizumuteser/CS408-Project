using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS408Project_Client
{
    public partial class Form1 : Form
    {
        bool terminating = false;
        bool connected = false;
        //bool attemptingConnect = false;

        Socket clientSocket;
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBoxIP.Text;
            int portNumber;
            string username = textBoxUsername.Text;
            string header = "CONNECT_REQUEST";
            //string headerListReq = "REQUESTFRIENDS";

            if(username == "")
            {
                richTextBox.AppendText("You must enter a username\n");
            }

            else if (Int32.TryParse(textBoxPort.Text, out portNumber))
            {
                try
                {
                    clientSocket.Connect(IP, portNumber);
                    Byte[] buffer = Encoding.Default.GetBytes(header + '|' + username);

                    clientSocket.Send(buffer);

                 

                    //textBoxUsername.Enabled = true;

                    //attemptingConnect = true;
                    connected = true;

                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();
                }
                catch
                {
                    richTextBox.AppendText("Couldn't connect to the server.\n"); //
                }
            }
            else
            {
                richTextBox.AppendText("Check the port number!\n");
            }
        }

        private void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[4096];
                    clientSocket.Receive(buffer);
                    Console.WriteLine(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));



                    if (incomingMessage[0] == '1')
                    {
                        string[] messages = incomingMessage.Split('|');
                        richTextBox.AppendText("[Server]: " + messages[1]);
                        //connected = true;
                        //attemptingConnect = false;

                        buttonDisconnect.Enabled = true;
                        buttonConnect.Enabled = false;
                        textBoxIP.Enabled = false;
                        textBoxPort.Enabled = false;
                        textBoxUsername.Enabled = false;

                        textBoxPost.Enabled = true;
                        buttonAllPosts.Enabled = true;
                        buttonSend.Enabled = true;
                    }

                    else if (incomingMessage[0] == '0')
                    {
                        string[] messages = incomingMessage.Split('|');
                        richTextBox.AppendText("[Server]: " + messages[1]);
                        connected = false;
                        //attemptingConnect = false;
                    }

                    else if (incomingMessage[0] == '2') //retreive all post
                    {
                        string[] messages = incomingMessage.Split('$');
                        string username = textBoxUsername.Text;
                        String[] allPosts = messages[1].Split('\n');
                        richTextBox.AppendText("Showing all posts from clients:\n");


                        for (int i = 0; i < allPosts.Length - 1; i++)
                        {
                            //Console.WriteLine(allPosts[i]);
                            string[] messageParts = allPosts[i].Split('|');

                            if (messageParts[0] == username)
                            {
                                continue;
                            }
                            richTextBox.AppendText("Username: " + messageParts[0] + '\n');
                            richTextBox.AppendText("Post ID: " + messageParts[1] + '\n');
                            richTextBox.AppendText("Post: " + messageParts[2] + '\n');
                            richTextBox.AppendText("Time: " + messageParts[3] + "\n\n");
                        }
                    }

                    else if (incomingMessage[0] == '3') //deletePost, 
                    {
                        string[] messages = incomingMessage.Split('|');
                        richTextBox.AppendText("[Server]: " + messages[1]);
                    }

                    else if (incomingMessage[0] == '4') //show your friend list
                    {

                        listBoxOfFriends.Items.Clear();

                        string[] messages = incomingMessage.Split('$');
                        string[] firstDivision = messages[1].Split('|');

                        for (int i = 0; i < firstDivision.Length; i++)
                        {
                            listBoxOfFriends.Items.Add(messages[i]);
                        }

                    }
                    else if (incomingMessage[0] == '6') //friendpost
                    {

                        string[] messages = incomingMessage.Split('$');
                        string username = textBoxUsername.Text;
                        String[] allPosts = messages[1].Split('\n');
                        richTextBox.AppendText("Showing all posts from clients:\n");


                        for (int i = 0; i < allPosts.Length - 1; i++)
                        {
                            //Console.WriteLine(allPosts[i]);
                            string[] messageParts = allPosts[i].Split('|');

                            foreach (var user in listBoxOfFriends.Items)
                            {
                                if (messageParts[0] == username)
                                {
                                    richTextBox.AppendText("Username: " + messageParts[0] + '\n');
                                    richTextBox.AppendText("Post ID: " + messageParts[1] + '\n');
                                    richTextBox.AppendText("Post: " + messageParts[2] + '\n');
                                    richTextBox.AppendText("Time: " + messageParts[3] + "\n\n");
                                }
                            }
                        }
                    }

                    else if (incomingMessage[0] == '7') //mypost
                    {

                        string[] messages = incomingMessage.Split('$');
                        string username = textBoxUsername.Text;
                        String[] allPosts = messages[1].Split('\n');
                        richTextBox.AppendText("Showing all posts from clients:\n");


                        for (int i = 0; i < allPosts.Length - 1; i++)
                        {
                            //Console.WriteLine(allPosts[i]);
                            string[] messageParts = allPosts[i].Split('|');

                            if (messageParts[0] == username)
                            {
                                richTextBox.AppendText("Username: " + messageParts[0] + '\n');
                                richTextBox.AppendText("Post ID: " + messageParts[1] + '\n');
                                richTextBox.AppendText("Post: " + messageParts[2] + '\n');
                                richTextBox.AppendText("Time: " + messageParts[3] + "\n\n");
                            }

                        }
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        //richTextBox.AppendText("The server has disconnected.\n");

                        textBoxUsername.Enabled = true;
                        textBoxIP.Enabled = true;
                        textBoxPort.Enabled = true;

                        buttonDisconnect.Enabled = false;
                        buttonConnect.Enabled = true;

                        textBoxPost.Enabled = false;
                        buttonAllPosts.Enabled = false;
                        buttonSend.Enabled = false;
                    }

                    clientSocket.Close();
                    connected = false;
                    //attemptingConnect = false; //
                }
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string message = textBoxPost.Text;
            string username = textBoxUsername.Text;
            string header = "POST";

            if (connected)
            {
                string currentTime = DateTime.Now.ToString("s");
                //Console.WriteLine(currentTime + '\n'); //
                string toSend = header + "|" + username + "|" + message + "|" + currentTime;
                Byte[] buffer = Encoding.Default.GetBytes(toSend);
                clientSocket.Send(buffer);
                richTextBox.AppendText("You have succesfully sent a post!" + '\n');
                richTextBox.AppendText(username + ": " + message + '\n');
            }
        }
        /*
        private void buttonAllPosts_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string header = "RETRIEVE_ALL";

            if (connected)
            {
                string toSend = header + "|" + username;
                Byte[] buffer = Encoding.Default.GetBytes(toSend);
                clientSocket.Send(buffer);
            }
        }
        */

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            connected = false;
            //attemptingConnect = false;
            terminating = true;

            string header = "DISCONNECTED";
            string username = textBoxUsername.Text;
            Byte[] buffer = Encoding.Default.GetBytes(header + '|' + username);
            clientSocket.Send(buffer);

            clientSocket.Close();
            richTextBox.AppendText("Successfully disconnected\n");

            textBoxPost.Enabled = false;

            textBoxUsername.Enabled = true;
            textBoxIP.Enabled = true;
            textBoxPort.Enabled = true;

            buttonAllPosts.Enabled = false;
            buttonSend.Enabled = false;
            buttonDisconnect.Enabled = false;
            buttonConnect.Enabled = true;
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (connected)
            {
                string header = "DISCONNECTED";
                string username = textBoxUsername.Text;
                Byte[] buffer = Encoding.Default.GetBytes(header + '|' + username);
                clientSocket.Send(buffer);
            }
            connected = false;
            //attemptingConnect = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void buttonAddFriend_Click_1(object sender, EventArgs e)
        {
            if (connected)
            {
                string header = "ADDFRIEND";
                string myUsername = textBoxUsername.Text;
                string friendUsername = textBoxAddFriend.Text;
                Byte[] buffer = Encoding.Default.GetBytes(header + '|' + myUsername + '|' +  friendUsername);
                clientSocket.Send(buffer);


            }
        }

        private void buttonDeletePost_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                string header = "DELETEPOST";
                string myUsername = textBoxUsername.Text;
                string toBeDeletedID = textBoxPostDelete.Text;
                Byte[] buffer = Encoding.Default.GetBytes(header + '|' + myUsername + '|' + toBeDeletedID);
                clientSocket.Send(buffer);
            }

        }

        private void buttonFriendsPosts_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string header = "RETRIEVE_FRIENDPOST";

            if (connected)
            {
                string toSend = header + "|" + username;
                Byte[] buffer = Encoding.Default.GetBytes(toSend);
                clientSocket.Send(buffer);
            }

        }
        
      
        private void buttonAllPosts_Click_1(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string header = "RETRIEVE_ALL";

            if (connected)
            {
                string toSend = header + "|" + username;
                Byte[] buffer = Encoding.Default.GetBytes(toSend);
                clientSocket.Send(buffer);
            }

        }

        private void buttonMyPosts_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string header = "RETRIEVE_MYPOST";

            if (connected)
            {
                string toSend = header + "|" + username;
                Byte[] buffer = Encoding.Default.GetBytes(toSend);
                clientSocket.Send(buffer);
            }

        }

        private void buttonRemoveFriend_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                string header = "REMOVEFRIEND";
                string myUsername = textBoxUsername.Text;
                string friendUsername = listBoxOfFriends.Text;
                Byte[] buffer = Encoding.Default.GetBytes(header + '|' + myUsername + '|' + friendUsername);
                clientSocket.Send(buffer);
            }

        }
    }
}
