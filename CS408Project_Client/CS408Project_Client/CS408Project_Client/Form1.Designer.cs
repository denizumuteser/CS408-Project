
namespace CS408Project_Client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPost = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.buttonRemoveFriend = new System.Windows.Forms.Button();
            this.buttonFriendsPosts = new System.Windows.Forms.Button();
            this.buttonMyPosts = new System.Windows.Forms.Button();
            this.textBoxPostDelete = new System.Windows.Forms.TextBox();
            this.textBoxAddFriend = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonAllPosts = new System.Windows.Forms.Button();
            this.buttonAddFriend = new System.Windows.Forms.Button();
            this.buttonDeletePost = new System.Windows.Forms.Button();
            this.listBoxOfFriends = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(99, 39);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(152, 20);
            this.textBoxIP.TabIndex = 0;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(99, 88);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(152, 20);
            this.textBoxPort.TabIndex = 1;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(99, 137);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(152, 20);
            this.textBoxUsername.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Username:";
            // 
            // textBoxPost
            // 
            this.textBoxPost.Location = new System.Drawing.Point(99, 456);
            this.textBoxPost.Name = "textBoxPost";
            this.textBoxPost.Size = new System.Drawing.Size(156, 20);
            this.textBoxPost.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 505);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Post ID:";
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(434, 32);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(287, 360);
            this.richTextBox.TabIndex = 8;
            this.richTextBox.Text = "";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(274, 37);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 9;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(274, 86);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(75, 23);
            this.buttonDisconnect.TabIndex = 10;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Enabled = false;
            this.buttonSend.Location = new System.Drawing.Point(274, 458);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 11;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // buttonRemoveFriend
            // 
            this.buttonRemoveFriend.Enabled = false;
            this.buttonRemoveFriend.Location = new System.Drawing.Point(125, 347);
            this.buttonRemoveFriend.Name = "buttonRemoveFriend";
            this.buttonRemoveFriend.Size = new System.Drawing.Size(105, 23);
            this.buttonRemoveFriend.TabIndex = 16;
            this.buttonRemoveFriend.Text = "Remove Friend";
            this.buttonRemoveFriend.UseVisualStyleBackColor = true;
            this.buttonRemoveFriend.Click += new System.EventHandler(this.buttonRemoveFriend_Click);
            // 
            // buttonFriendsPosts
            // 
            this.buttonFriendsPosts.Enabled = false;
            this.buttonFriendsPosts.Location = new System.Drawing.Point(594, 426);
            this.buttonFriendsPosts.Name = "buttonFriendsPosts";
            this.buttonFriendsPosts.Size = new System.Drawing.Size(112, 23);
            this.buttonFriendsPosts.TabIndex = 17;
            this.buttonFriendsPosts.Text = "Firend\'s Posts";
            this.buttonFriendsPosts.UseVisualStyleBackColor = true;
            this.buttonFriendsPosts.Click += new System.EventHandler(this.buttonFriendsPosts_Click);
            // 
            // buttonMyPosts
            // 
            this.buttonMyPosts.Enabled = false;
            this.buttonMyPosts.Location = new System.Drawing.Point(544, 487);
            this.buttonMyPosts.Name = "buttonMyPosts";
            this.buttonMyPosts.Size = new System.Drawing.Size(75, 23);
            this.buttonMyPosts.TabIndex = 18;
            this.buttonMyPosts.Text = "My Posts";
            this.buttonMyPosts.UseVisualStyleBackColor = true;
            this.buttonMyPosts.Click += new System.EventHandler(this.buttonMyPosts_Click);
            // 
            // textBoxPostDelete
            // 
            this.textBoxPostDelete.Location = new System.Drawing.Point(99, 502);
            this.textBoxPostDelete.Name = "textBoxPostDelete";
            this.textBoxPostDelete.Size = new System.Drawing.Size(156, 20);
            this.textBoxPostDelete.TabIndex = 19;
            // 
            // textBoxAddFriend
            // 
            this.textBoxAddFriend.Location = new System.Drawing.Point(99, 408);
            this.textBoxAddFriend.Name = "textBoxAddFriend";
            this.textBoxAddFriend.Size = new System.Drawing.Size(156, 20);
            this.textBoxAddFriend.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 463);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Post:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 411);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Username:";
            // 
            // buttonAllPosts
            // 
            this.buttonAllPosts.Enabled = false;
            this.buttonAllPosts.Location = new System.Drawing.Point(459, 426);
            this.buttonAllPosts.Name = "buttonAllPosts";
            this.buttonAllPosts.Size = new System.Drawing.Size(98, 23);
            this.buttonAllPosts.TabIndex = 23;
            this.buttonAllPosts.Text = "All Posts";
            this.buttonAllPosts.UseVisualStyleBackColor = true;
            this.buttonAllPosts.Click += new System.EventHandler(this.buttonAllPosts_Click_1);
            // 
            // buttonAddFriend
            // 
            this.buttonAddFriend.Enabled = false;
            this.buttonAddFriend.Location = new System.Drawing.Point(274, 406);
            this.buttonAddFriend.Name = "buttonAddFriend";
            this.buttonAddFriend.Size = new System.Drawing.Size(75, 23);
            this.buttonAddFriend.TabIndex = 24;
            this.buttonAddFriend.Text = "Add Friend";
            this.buttonAddFriend.UseVisualStyleBackColor = true;
            this.buttonAddFriend.Click += new System.EventHandler(this.buttonAddFriend_Click_1);
            // 
            // buttonDeletePost
            // 
            this.buttonDeletePost.Enabled = false;
            this.buttonDeletePost.Location = new System.Drawing.Point(274, 505);
            this.buttonDeletePost.Name = "buttonDeletePost";
            this.buttonDeletePost.Size = new System.Drawing.Size(75, 23);
            this.buttonDeletePost.TabIndex = 25;
            this.buttonDeletePost.Text = "Delete";
            this.buttonDeletePost.UseVisualStyleBackColor = true;
            this.buttonDeletePost.Click += new System.EventHandler(this.buttonDeletePost_Click);
            // 
            // listBoxOfFriends
            // 
            this.listBoxOfFriends.FormattingEnabled = true;
            this.listBoxOfFriends.Location = new System.Drawing.Point(111, 195);
            this.listBoxOfFriends.Name = "listBoxOfFriends";
            this.listBoxOfFriends.Size = new System.Drawing.Size(129, 134);
            this.listBoxOfFriends.Sorted = true;
            this.listBoxOfFriends.TabIndex = 26;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 626);
            this.Controls.Add(this.listBoxOfFriends);
            this.Controls.Add(this.buttonDeletePost);
            this.Controls.Add(this.buttonAddFriend);
            this.Controls.Add(this.buttonAllPosts);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxAddFriend);
            this.Controls.Add(this.textBoxPostDelete);
            this.Controls.Add(this.buttonMyPosts);
            this.Controls.Add(this.buttonFriendsPosts);
            this.Controls.Add(this.buttonRemoveFriend);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxIP);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button buttonRemoveFriend;
        private System.Windows.Forms.Button buttonFriendsPosts;
        private System.Windows.Forms.Button buttonMyPosts;
        private System.Windows.Forms.TextBox textBoxPostDelete;
        private System.Windows.Forms.TextBox textBoxAddFriend;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonAllPosts;
        private System.Windows.Forms.Button buttonAddFriend;
        private System.Windows.Forms.Button buttonDeletePost;
        private System.Windows.Forms.ListBox listBoxOfFriends;
    }
}

