using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Net.Sockets;

using Sytel.Mdn;
using Sytel.Mdn.Sockets;
using Sytel.Mdn.SDConnection;

namespace DialerChat
{
	/// <summary>
	/// Summary description for DialerChatForm.
	/// </summary>
	public class DialerChatForm : System.Windows.Forms.Form
	{
		private IContainer components;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox MessageToSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox HostName;
		private System.Windows.Forms.ListBox MessageHistory;
		private System.Windows.Forms.Label ConnectionStatus;
		private System.Windows.Forms.Button SendMessage;
		private System.Windows.Forms.RadioButton Connect;
		private System.Windows.Forms.RadioButton Disconnect;

		//TcpClient HostSocket;
		//MessageReaderWriter HostReaderWriter;
		SoftdialConnection m_HostConnection;

		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem FileExit;
		private System.Windows.Forms.MenuItem GetSelection;
        private Label label2;
        private TextBox TenantID;
        private Label label5;
        private TextBox Password;
        private Label label3;
        private TextBox UserName;
        private Label label6;
        private TextBox PortNumber;
		private MenuItem menuItem2;
		bool Connected = false;

		#region Constructor
		/// <summary>Initialise the form and its instance members</summary>
		public DialerChatForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			MessageToSend.KeyDown +=new KeyEventHandler(MessageToSend_KeyDown);

			//LineTerminator.Text = "NULL";
		}
		#endregion // Constructor

		#region Dispose
		/// <summary>Clean up any resources being used.</summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion // Dispose

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.MessageHistory = new System.Windows.Forms.ListBox();
			this.MessageToSend = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.PortNumber = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.Password = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.UserName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.TenantID = new System.Windows.Forms.TextBox();
			this.Disconnect = new System.Windows.Forms.RadioButton();
			this.Connect = new System.Windows.Forms.RadioButton();
			this.ConnectionStatus = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.HostName = new System.Windows.Forms.TextBox();
			this.SendMessage = new System.Windows.Forms.Button();
			this.mainMenu1 = new System.Windows.Forms.MainMenu( this.components );
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.FileExit = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.GetSelection = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MessageHistory
			// 
			this.MessageHistory.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.MessageHistory.BackColor = System.Drawing.Color.FromArgb( ((int) (((byte) (224)))), ((int) (((byte) (224)))), ((int) (((byte) (224)))) );
			this.MessageHistory.ColumnWidth = 100;
			this.MessageHistory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.MessageHistory.IntegralHeight = false;
			this.MessageHistory.Location = new System.Drawing.Point( 9, 161 );
			this.MessageHistory.Name = "MessageHistory";
			this.MessageHistory.Size = new System.Drawing.Size( 421, 197 );
			this.MessageHistory.TabIndex = 1;
			this.MessageHistory.DrawItem += new System.Windows.Forms.DrawItemEventHandler( this.DrawItemHandler );
			// 
			// MessageToSend
			// 
			this.MessageToSend.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.MessageToSend.BackColor = System.Drawing.SystemColors.Window;
			this.MessageToSend.Location = new System.Drawing.Point( 9, 365 );
			this.MessageToSend.Name = "MessageToSend";
			this.MessageToSend.Size = new System.Drawing.Size( 327, 20 );
			this.MessageToSend.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBox1.Controls.Add( this.PortNumber );
			this.groupBox1.Controls.Add( this.label6 );
			this.groupBox1.Controls.Add( this.label5 );
			this.groupBox1.Controls.Add( this.Password );
			this.groupBox1.Controls.Add( this.label3 );
			this.groupBox1.Controls.Add( this.UserName );
			this.groupBox1.Controls.Add( this.label2 );
			this.groupBox1.Controls.Add( this.TenantID );
			this.groupBox1.Controls.Add( this.Disconnect );
			this.groupBox1.Controls.Add( this.Connect );
			this.groupBox1.Controls.Add( this.ConnectionStatus );
			this.groupBox1.Controls.Add( this.label4 );
			this.groupBox1.Controls.Add( this.label1 );
			this.groupBox1.Controls.Add( this.HostName );
			this.groupBox1.Location = new System.Drawing.Point( 10, 0 );
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size( 418, 155 );
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Connection control";
			// 
			// PortNumber
			// 
			this.PortNumber.Location = new System.Drawing.Point( 75, 45 );
			this.PortNumber.Name = "PortNumber";
			this.PortNumber.Size = new System.Drawing.Size( 159, 20 );
			this.PortNumber.TabIndex = 18;
			this.PortNumber.Text = "6500";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point( 6, 48 );
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size( 52, 18 );
			this.label6.TabIndex = 17;
			this.label6.Text = "Port";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point( 6, 123 );
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size( 63, 18 );
			this.label5.TabIndex = 15;
			this.label5.Text = "User Name";
			// 
			// Password
			// 
			this.Password.Location = new System.Drawing.Point( 75, 120 );
			this.Password.Name = "Password";
			this.Password.PasswordChar = '*';
			this.Password.Size = new System.Drawing.Size( 159, 20 );
			this.Password.TabIndex = 16;
			this.Password.Text = "SYSTEM";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point( 6, 99 );
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size( 63, 18 );
			this.label3.TabIndex = 13;
			this.label3.Text = "User Name";
			// 
			// UserName
			// 
			this.UserName.Location = new System.Drawing.Point( 75, 96 );
			this.UserName.Name = "UserName";
			this.UserName.Size = new System.Drawing.Size( 159, 20 );
			this.UserName.TabIndex = 14;
			this.UserName.Text = "SYSTEM";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point( 6, 73 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size( 52, 18 );
			this.label2.TabIndex = 11;
			this.label2.Text = "Tenant";
			// 
			// TenantID
			// 
			this.TenantID.Location = new System.Drawing.Point( 75, 70 );
			this.TenantID.Name = "TenantID";
			this.TenantID.Size = new System.Drawing.Size( 159, 20 );
			this.TenantID.TabIndex = 12;
			this.TenantID.Text = "landlord";
			// 
			// Disconnect
			// 
			this.Disconnect.Appearance = System.Windows.Forms.Appearance.Button;
			this.Disconnect.Checked = true;
			this.Disconnect.Location = new System.Drawing.Point( 325, 22 );
			this.Disconnect.Name = "Disconnect";
			this.Disconnect.Size = new System.Drawing.Size( 84, 28 );
			this.Disconnect.TabIndex = 10;
			this.Disconnect.TabStop = true;
			this.Disconnect.Text = "Disconnect";
			this.Disconnect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Connect
			// 
			this.Connect.Appearance = System.Windows.Forms.Appearance.Button;
			this.Connect.Location = new System.Drawing.Point( 241, 22 );
			this.Connect.Name = "Connect";
			this.Connect.Size = new System.Drawing.Size( 84, 28 );
			this.Connect.TabIndex = 9;
			this.Connect.Text = "Connect";
			this.Connect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Connect.CheckedChanged += new System.EventHandler( this.Connect_CheckedChanged );
			// 
			// ConnectionStatus
			// 
			this.ConnectionStatus.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ConnectionStatus.BackColor = System.Drawing.Color.Plum;
			this.ConnectionStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ConnectionStatus.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)) );
			this.ConnectionStatus.Location = new System.Drawing.Point( 240, 105 );
			this.ConnectionStatus.Name = "ConnectionStatus";
			this.ConnectionStatus.Size = new System.Drawing.Size( 168, 38 );
			this.ConnectionStatus.TabIndex = 7;
			this.ConnectionStatus.Text = "Not yet connected";
			this.ConnectionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.Location = new System.Drawing.Point( 275, 83 );
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size( 96, 22 );
			this.label4.TabIndex = 6;
			this.label4.Text = "Connection status:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point( 6, 25 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 52, 18 );
			this.label1.TabIndex = 0;
			this.label1.Text = "Host";
			// 
			// HostName
			// 
			this.HostName.Location = new System.Drawing.Point( 75, 20 );
			this.HostName.Name = "HostName";
			this.HostName.Size = new System.Drawing.Size( 159, 20 );
			this.HostName.TabIndex = 1;
			this.HostName.Text = "localhost";
			this.HostName.DoubleClick += new System.EventHandler( this.SendMessage_Click );
			// 
			// SendMessage
			// 
			this.SendMessage.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.SendMessage.Location = new System.Drawing.Point( 346, 364 );
			this.SendMessage.Name = "SendMessage";
			this.SendMessage.Size = new System.Drawing.Size( 88, 26 );
			this.SendMessage.TabIndex = 3;
			this.SendMessage.Text = "Send";
			this.SendMessage.Click += new System.EventHandler( this.SendMessage_Click );
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem3} );
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.FileExit} );
			this.menuItem1.Text = "File";
			// 
			// FileExit
			// 
			this.FileExit.Index = 0;
			this.FileExit.Text = "E&xit";
			this.FileExit.Click += new System.EventHandler( this.FileExit_Click );
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.GetSelection,
            this.menuItem2} );
			this.menuItem3.Text = "&Command";
			// 
			// GetSelection
			// 
			this.GetSelection.Index = 0;
			this.GetSelection.Shortcut = System.Windows.Forms.Shortcut.F3;
			this.GetSelection.Text = "Get &Selection";
			this.GetSelection.Click += new System.EventHandler( this.menuItem4_Click );
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.F4;
			this.menuItem2.Text = "&Copy Selection to Clipboard";
			this.menuItem2.Click += new System.EventHandler( this.menuItem2_Click );
			// 
			// DialerChatForm
			// 
			this.AcceptButton = this.SendMessage;
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.ClientSize = new System.Drawing.Size( 439, 397 );
			this.Controls.Add( this.SendMessage );
			this.Controls.Add( this.groupBox1 );
			this.Controls.Add( this.MessageToSend );
			this.Controls.Add( this.MessageHistory );
			this.Menu = this.mainMenu1;
			this.MinimumSize = new System.Drawing.Size( 368, 256 );
			this.Name = "DialerChatForm";
			this.Text = "Dialer Chat";
			this.groupBox1.ResumeLayout( false );
			this.groupBox1.PerformLayout();
			this.ResumeLayout( false );
			this.PerformLayout();

		}
		#endregion

		#region Main function
		/// <summary>The main entry point for the application.</summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new DialerChatForm());
		}
		#endregion // Main

		#region Methods
		/// <summary>Adds a message to the list control, scrolls to the bottom and if there are more than 4096 lines in the list
		/// remove the first one.</summary>
		/// <param name="message">The message to display</param>
		/// <param name="outgoing">true if the message is outgoing, false for incoming messages</param>
		public void AddMessageToHistory( string message, bool outgoing )
		{
			ListBox mh = MessageHistory;
			
			mh.BeginUpdate();

			AddColoredLine(new ColoredLine(message, (outgoing == true ? MessageFlag.Outgoing : MessageFlag.Incoming)	));

			if( mh.Items.Count > 4096 ) 
			{
				mh.Items.RemoveAt( 0 );
			}

			if (mh.ItemHeight > 0 &&
				mh.Items.Count > (mh.Height / mh.ItemHeight)
				)
			{
				mh.TopIndex = mh.Items.Count - (mh.Height / mh.ItemHeight);
			}
			mh.SelectedIndex = mh.Items.Count - 1;

			mh.EndUpdate();
		}

		public class ColoredLine
		{
			public string Text;
			public MessageFlag TextColor;

			public ColoredLine( string s, MessageFlag mf )
			{
				Text = s;
				TextColor = mf;
			}
			public override string ToString()
			{
				return Text;
			}

		}

		private void DrawItemHandler(object sender, DrawItemEventArgs e) 
		{ 
			// BUG: GJP At some stage need to give a better treatment.
			e.DrawBackground(); 
			e.DrawFocusRectangle();
			bool mSelected = (e.State & DrawItemState.Selected) != 0;

			if (e.Index == -1)
			{
				return;
			}
 
			ColoredLine cl = (ColoredLine)((ListBox)sender).Items[ e.Index ];

			Color mColor;

			Rectangle textBounds = e.Bounds;

			switch( cl.TextColor )
			{
				case (MessageFlag.Incoming) :
                    if (cl.Text.StartsWith(@"ER\"))
                        mColor = mSelected ? Color.Pink : Color.Red;
                    else
                    {
                        mColor = mSelected ? Color.White : Color.Maroon;
                    }
                    textBounds = new Rectangle(e.Bounds.Left + 32, e.Bounds.Top, Math.Max(0, e.Bounds.Width - 32), e.Bounds.Height);
					break;
				case (MessageFlag.Outgoing) :
                    mColor = mSelected ? Color.PaleTurquoise : Color.DarkBlue;
					break;
				case (MessageFlag.System) :
                    mColor = mSelected ? Color.PaleGreen : Color.Green;
					break;

				default:
					mColor = Color.Black;
					break;
			}

			using (SolidBrush sb = new SolidBrush(mColor))
			{
				e.Graphics.DrawString( cl.Text, e.Font, sb, textBounds );				
			}
		} 
		public enum MessageFlag
		{
			Incoming,
			Outgoing,
			System
		}
		private void AddColoredLine(ColoredLine cl)
		{
			MessageHistory.Items.Add (cl);
		}

		/// <summary>Establishes a TCP/IP connection to the remote host</summary>
		/// <returns>True if the connection was successful</returns>
		public bool ConnectToHost()
		{
			// No need to do anything if already connected
			if( Connected ) return true;

			// Ensure connection is fully cleaned up

			string host = HostName.Text;
			int port = Convert.ToInt32(PortNumber.Text);
            string tenant = TenantID.Text;
            string username = UserName.Text;
            string password = Password.Text;
            /*
                                    string terminator;

                                    switch( this.LineTerminator.Text )
                                    {
                                        case "CRLF": terminator = "\r\n";
                                            break;

                                        case "LFCR": terminator = "\n\r";
                                            break;

                                        case "LF": terminator = "\n";
                                            break;

                                        case "CR": terminator = "\r";
                                            break;

                                        default: terminator = "\0";
                                            break;
                                    }
                                    */
			try
			{
				m_HostConnection = new SoftdialConnection();

				m_HostConnection.HostName = host;
				m_HostConnection.PortNumber = port;

				m_HostConnection.MessageHandlerObject = this;

				if (m_HostConnection.ConnectSecure(tenant, username, password) )
                {
    				Connected = true;

    				ConnectionStatus.Text = string.Format( "Connected to '{0}' port {1}", host, port );

	    			Text = string.Format( "Dialer Chat [{0}]", host );

    				Connect.Checked = true;
	    			EnableDisableConnectControls( false );
                }
                else
                {
          			ConnectionStatus.Text = "Disconnected";
                    return false;
                }
			}
			catch( Exception e )
			{
				ConnectionStatus.Text = e.Message;
				return false;
			}

			return true;
		}

		/// <summary>Handles all dialer messages not passed by handler.</summary>
		public void UnhandledMessage(SoftdialMessage sm)
		{
			string mMessage;

			if (sm.Attachment != null)
			{
				mMessage = string.Format("{0}\\FM\"{1}\"", sm.Message, sm.Attachment);
			}
			else
			{
				mMessage = sm.Message;
			}

			AddMessageToHistory( mMessage, false );
		}

		public void ConnectionClosed()
		{
			// Note this gets called dynamically on connection close.
			if (null != m_HostConnection)
			{
				m_HostConnection.Disconnect();
				m_HostConnection.Dispose();
				m_HostConnection = null;
			}

			Connected = false;

			ConnectionStatus.Text = "Disconnected";

			Text = @"Dialer Chat";

			Disconnect.Checked = true;
			EnableDisableConnectControls( true );
			
		}

		/// <summary>Close the remote connection (if there is one)</summary>
		public void DisconnectFromHost()
		{
            ConnectionClosed();
		}

		/// <summary>Change the Enabled property of protocol/connection releated controls</summary>
		/// <param name="enable">True to enable the control, false to disable them</param>
		public void EnableDisableConnectControls( bool enable )
		{
			HostName.Enabled = enable;
            Password.Enabled = enable;
            UserName.Enabled = enable;
            TenantID.Enabled = enable;
            PortNumber.Enabled = enable;
		}
		#endregion // Methods
	
		#region Control event handlers
		/// <summary>Handle the send message button being clicked</summary>
		/// <param name="sender">Reference to the object firing this event</param>
		/// <param name="e">Standard event arguments</param>
		private void SendMessage_Click(object sender, System.EventArgs e)
		{
			string message = this.MessageToSend.Text;

			if( Connected == false )
			{
				if( ConnectToHost() == false ) return;
			}

			if (message.IndexOf('¬') != -1)
			{
				// Multiple messages
				string[] messages = message.Split('¬');
				for( int i = 0 ; i < messages.Length ; i++ )
				{
					try
					{
						m_HostConnection.Send( messages[i] );
					}
					catch( Exception )
					{
						DisconnectFromHost();
					}

                    AddMessageToHistory(messages[i], true);
				}
			}
			else
			{
				try
				{
					m_HostConnection.Send( message );
				}
				catch( Exception )
				{
					DisconnectFromHost();
				}
			
				AddMessageToHistory( message, true );
			}
			
			MessageToSend.Text = string.Empty;
			MessageToSend.Focus();
		}

		/// <summary>Handle the connect checkbox changing state</summary>
		/// <param name="sender">Reference to the object firing this event</param>
		/// <param name="e">Standard event arguments</param>
		private void Connect_CheckedChanged(object sender, System.EventArgs e)
		{
			if( Connect.Checked ) 
			{
				ConnectToHost();
			}
			else 
			{
				DisconnectFromHost();
			}
		}

		private void FileExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			// Get Selection!!!
			int mSel = MessageHistory.SelectedIndex;
			ColoredLine mSelLine;

			while (mSel >= 0)
			{
				mSelLine = (ColoredLine) MessageHistory.Items[mSel];
				if (mSelLine.TextColor == MessageFlag.Outgoing)
				{
					MessageToSend.Text = MessageHistory.Items[mSel].ToString();
					MessageToSend.Select(MessageToSend.Text.Length, 0);
					MessageHistory.SelectedIndex = mSel;
					break;
				}
				mSel--;
			}
		}


		// Copy to clipboard
		private void menuItem2_Click( object sender, EventArgs e )
		{
			object item = MessageHistory.SelectedItem;
			if (item != null)
			{
				Clipboard.SetText( item.ToString() );
			}
		}

		private void MoveSelUp()
		{
			int mSel = MessageHistory.SelectedIndex;
			ColoredLine mSelLine;

			while (mSel > 0)
			{
				mSel--;
				mSelLine = (ColoredLine) MessageHistory.Items[mSel];
				if (mSelLine.TextColor == MessageFlag.Outgoing)
				{
					MessageToSend.Text = MessageHistory.Items[mSel].ToString();
					MessageToSend.Select(MessageToSend.Text.Length, 0);
					MessageHistory.SelectedIndex = mSel;
					break;
				}
			}
		}

		private void MoveSelDown()
		{
			int mSel = MessageHistory.SelectedIndex;
			ColoredLine mSelLine;

			while (mSel < (MessageHistory.Items.Count - 1) )
			{
				mSel++;
				mSelLine = (ColoredLine) MessageHistory.Items[mSel];
				if (mSelLine.TextColor == MessageFlag.Outgoing)
				{
					MessageToSend.Text = MessageHistory.Items[mSel].ToString();
					MessageToSend.Select(MessageToSend.Text.Length, 0);
					MessageHistory.SelectedIndex = mSel;
					break;
				}
			}
		}

		private void MessageToSend_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
			{
				MoveSelUp();
				e.Handled = true;
			}

			if (e.KeyCode == Keys.Down)
			{
				MoveSelDown();
				e.Handled = true;
			}
		}
		#endregion //Control event handlers

	}
}



