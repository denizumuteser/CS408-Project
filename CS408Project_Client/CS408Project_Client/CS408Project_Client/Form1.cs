﻿using System;
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

            if(username == "")
            {
                richTextBox.AppendText("You must enter a username");
            }

            else if (Int32.TryParse(textBoxPort.Text, out portNumber))
            {
                try
                {
                    clientSocket.Connect(IP, portNumber);
<<<<<<< HEAD
                    Byte[] buffer = Encoding.Default.GetBytes(header + '|' + username);
=======
                    Byte[] buffer = Encoding.Default.GetBytes(header+"|"+username);
>>>>>>> 4ed990fd1483ece3a04b062606b1393cee3b078d
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

                    }

                    else if (incomingMessage[0] == '0')
                    {
                        string[] messages = incomingMessage.Split('|');
                        richTextBox.AppendText("[Server]: " + messages[1]);
                        connected = false;
                        //attemptingConnect = false;
                    }

                    else if (incomingMessage[0] == '2')
                    {
                        string[] messages = incomingMessage.Split('$');
                        string username = textBoxUsername.Text;
                        String[] allPosts = messages[1].Split('\n');
                        richTextBox.AppendText("Showing all posts from clients:\n");

                        for (int i = 0; i<allPosts.Length; i++)
                        {
                            string[] messageParts = allPosts[i].Split('|');
                            if(messageParts[0] == username)
                            {
                                continue;
                            }
                            richTextBox.AppendText("Username: " + allPosts[0] + '\n');
                            richTextBox.AppendText("Post ID: " + allPosts[1] + '\n');
                            richTextBox.AppendText("Post: " + allPosts[2] + '\n');
                            richTextBox.AppendText("Time: " + allPosts[3] + '\n');
                        }
                    }

                    else if (incomingMessage[0] == '3')
                    {
                        string[] messages = incomingMessage.Split('|');
                        richTextBox.AppendText("[Server]: " + messages[1]);
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        richTextBox.AppendText("The server has disconnected.\n");

                        textBoxUsername.Enabled = true;
                        textBoxIP.Enabled = true;
                        textBoxPort.Enabled = true;

                        buttonDisconnect.Enabled = false;
                        buttonConnect.Enabled = true;
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

            if (connected )
            {
                string currentTime = DateTime.Now.ToString("s");
                Console.WriteLine(currentTime + '\n'); //
                string toSend = header + "|" + username + "|" + message + "|" + currentTime;
                Byte[] buffer = Encoding.Default.GetBytes(toSend);
                clientSocket.Send(buffer);
            }
        }

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

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            connected = false;
            //attemptingConnect = false;
            terminating = true;
            clientSocket.Close();
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
            connected = false;
            //attemptingConnect = false;
            terminating = true;
            Environment.Exit(0);
        }
    }
}
