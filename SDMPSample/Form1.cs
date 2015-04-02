using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sytel.Mdn.Helpers;
using Sytel.Mdn.Extensions;
using Sytel.Mdn.SDConnection;

using CampaignManager;
using Management;

namespace SDMPSample
{
    public partial class Form1 : Form
    {
        private CampaignManagerClass m_CampaignManager;
        private ManagementClass m_Management;

		MethodDispatcher<Action<EventObject>> eventHandler;

		/// <summary>
		/// Event Handler Property
		/// </summary>
		protected MethodDispatcher<Action<EventObject>> EventHandler
		{
			get
			{
				if (eventHandler == null)
				{
					eventHandler = new MethodDispatcher<Action<EventObject>>();
				}

				return eventHandler;
			}
		}

        public Form1()
        {
            InitializeComponent();

            m_Management = new ManagementClass();
            m_CampaignManager = new CampaignManagerClass();

            m_Management.Connect();
            m_CampaignManager.Connect();

            m_Management.EventTenantData += new TenantDataEventHandler(m_Management_EventTenantData);
            m_CampaignManager.EventStatusBroadcast += new StatusBroadcastEventHandler(m_CampaignManager_EventStatusBroadcast);
            m_CampaignManager.EventPropertyData += new PropertyDataEventHandler(m_CampaignManager_EventPropertyData);
            m_CampaignManager.EventErrorReport += new CampaignManager.ErrorReportEventHandler(m_CampaignManager_EventErrorReport);
            m_CampaignManager.EventMessageVerify +=new CampaignManager.MessageVerifyEventHandler(m_CampaignManager_EventMessageVerify);
        }

        void  m_CampaignManager_EventMessageVerify(CampaignManagerSpace.EventMessageVerifyArgs args)
        {
			if (eventHandler == null) return;

            EventObject mEvent = new EventObject();

            mEvent.m_Token = args.Token;
            mEvent.m_Success = true;

            // Call the event handler
            eventHandler[args.Token](mEvent);
        }

        void  m_CampaignManager_EventErrorReport(CampaignManagerSpace.EventErrorReportArgs args)
        {
			if (eventHandler == null) return;
            string TK = "";

            EventObject mEvent = new EventObject();

            SoftdialMessage sm = new SoftdialMessage(args.FormattedMessage);

            sm.GetString("TK", ref TK);

            mEvent.m_Token = TK;
            mEvent.m_Success = false;
            mEvent.m_Error = args.ErrorCode;

            // Call the event handler
			eventHandler[TK]( mEvent );
        }

        void m_CampaignManager_EventPropertyData(CampaignManagerSpace.EventPropertyDataArgs args)
        {
            PropertyData.Text = args.Data;
        }

        void m_CampaignManager_EventStatusBroadcast(CampaignManagerSpace.EventStatusBroadcastArgs args)
        {
            CampaignProperties.Text = args.Data;
        }

        void m_Management_EventTenantData(ManagementSpace.EventTenantDataArgs args)
        {
            if (args.ServiceState == 2)
            {
                // add to list
                Tenants.Items.Add(args.TenantDescriptor);
            }
            else
            {
                Tenants.Items.Remove(args.TenantDescriptor);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CampaignManagerSpace.StatusRequestArgs mArgs = new CampaignManagerSpace.StatusRequestArgs();

            mArgs.TenantDescriptor = Tenants.Text;
            mArgs.TransactionIdentifier = "anothertrans";

            m_CampaignManager.StatusRequest(mArgs);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ManagementSpace.EnumerateTenantsArgs mArgs = new ManagementSpace.EnumerateTenantsArgs();

            mArgs.TransactionIdentifier = "mytransid";
            m_Management.EnumerateTenants(mArgs);
        }

        private int GetCampaignID()
        {
            return Convert.ToInt32(CampaignID.Text);
        }

        #region campaign lock & unlock
        private void LockTheCampaign( int pCampaignID, string pToken )
        {
            CampaignManagerSpace.LockCampaignArgs mArgs = new CampaignManagerSpace.LockCampaignArgs();
            mArgs.CampaignIdentifier = pCampaignID;
			mArgs.TenantDescriptor = Tenants.Text;
            mArgs.Token = pToken;

            m_CampaignManager.LockCampaign(mArgs);
        }

        private void UnlockTheCampaign()
        {
            CampaignManagerSpace.UnlockCampaignArgs mArgs = new CampaignManagerSpace.UnlockCampaignArgs();
            mArgs.CampaignIdentifier = Convert.ToInt32(CampaignID.Text);
			mArgs.TenantDescriptor = Tenants.Text;

            m_CampaignManager.UnlockCampaign(mArgs);

            CampaignID.Enabled = true;

        }
        #endregion campaign lock & unlock

        #region GetCampaignProperties & callbacks
        private void GetPropertiesLocked(EventObject pEvent)
        {
            if (pEvent.m_Success)
            {
                // disable UI to show
                CampaignID.Enabled = false;

                string mToken = string.Format(@"GP_{0}_{1}", GetCampaignID(), Tenants.Text);
                // Get Properties
                CampaignManagerSpace.GetCampaignPropertiesArgs mArgs = new CampaignManagerSpace.GetCampaignPropertiesArgs();
                mArgs.CampaignIdentifier = Convert.ToInt32(CampaignID.Text);
	            mArgs.TenantDescriptor = Tenants.Text;
                mArgs.Token = mToken;

                EventHandler[mToken] = GetPropertiesHandled;
                // Get the properties
                m_CampaignManager.GetCampaignProperties(mArgs);
            }
            else
            {
                MessageBox.Show(string.Format("Not able to grant lock on campaign id {0}", 
                    CampaignID.Text));
            }
        }

        private void GetPropertiesHandled(EventObject pEvent)
        {
            // Campaign lock can be released regardless of success or fail
            UnlockTheCampaign();

            // Note - success means PD message will follow. No need to handle here
            if (!pEvent.m_Success)
            {
                MessageBox.Show(string.Format("Unable to get properties for campaign id {0}",
                    CampaignID.Text));
            }

        }

        private void GetProperties_Click(object sender, EventArgs e)
        {
            string mToken = string.Format(@"LC_{0}_{1}", GetCampaignID(), Tenants.Text);

            EventHandler[mToken] = GetPropertiesLocked;
            
            LockTheCampaign( GetCampaignID(), mToken );
        }
        #endregion GetCampaignProperties & callbacks

        private void SetPropertiesLocked(EventObject pEvent)
        {
            if (pEvent.m_Success)
            {
                // disable UI to show
                CampaignID.Enabled = false;

                string mToken = string.Format(@"SP_{0}_{1}", GetCampaignID(), Tenants.Text);
                // Set Properties
                CampaignManagerSpace.SetCampaignPropertiesArgs mArgs = new CampaignManagerSpace.SetCampaignPropertiesArgs();
                mArgs.CampaignIdentifier = Convert.ToInt32(CampaignID.Text);
                mArgs.Data = PropertyData.Text;
			    mArgs.TenantDescriptor = Tenants.Text;
                mArgs.Token = mToken;

                EventHandler[mToken] = SetPropertiesHandled;
                // Get the properties
                m_CampaignManager.SetCampaignProperties(mArgs);
            }
            else
            {
                MessageBox.Show(string.Format("Not able to grant lock on campaign id {0}",
                    CampaignID.Text));
            }
        }

        private void SetPropertiesHandled(EventObject pEvent)
        {
            // Campaign lock can be released regardless of success or fail
            UnlockTheCampaign();

            // Note - success means PD message will follow. No need to handle here
            if (!pEvent.m_Success)
            {
                MessageBox.Show(string.Format("Unable to set properties for campaign id {0}",
                    CampaignID.Text));
            }

        }

        private void SetProperties_Click(object sender, EventArgs e)
        {
            string mToken = string.Format(@"LC_{0}_{1}", GetCampaignID(), Tenants.Text);

            EventHandler[mToken] = SetPropertiesLocked;

            LockTheCampaign(GetCampaignID(), mToken);
        }

        #region StartCampaign
        private void StartCampaignLocked(EventObject pEvent)
        {
            if (pEvent.m_Success)
            {
                // disable UI to show
                CampaignID.Enabled = false;

                string mToken = string.Format(@"OC_{0}_{1}", GetCampaignID(), Tenants.Text);
                // Set Properties
                CampaignManagerSpace.OpenCampaignArgs mArgs = new CampaignManagerSpace.OpenCampaignArgs();
                mArgs.CampaignIdentifier = Convert.ToInt32(CampaignID.Text);
                mArgs.TenantDescriptor = Tenants.Text;
                mArgs.Token = mToken;

                EventHandler[mToken] = StartCampaignHandled;
                // Get the properties
                m_CampaignManager.OpenCampaign(mArgs);
            }
            else
            {
                MessageBox.Show(string.Format("Not able to grant lock on campaign id {0}",
                    CampaignID.Text) );
            }
        }

        private void StartCampaignHandled(EventObject pEvent)
        {
            // Campaign lock can be released regardless of success or fail
            UnlockTheCampaign();

            // Note - success means PD message will follow. No need to handle here
            if (!pEvent.m_Success)
            {
                MessageBox.Show(string.Format("Unable to start campaign id {0}. Error {1} returned. Please check the Softdial Error lookup.",
                    CampaignID.Text,
                    pEvent.m_Error) );
            }

        }

        private void StartCampaign_Click(object sender, EventArgs e)
        {
            string mToken = string.Format(@"LC_{0}_{1}", GetCampaignID(), Tenants.Text);

            EventHandler[mToken] = StartCampaignLocked;

            LockTheCampaign(GetCampaignID(), mToken);
        }
        #endregion StartCampaign

        #region StopCampaign
        private void StopCampaignLocked(EventObject pEvent)
        {
            if (pEvent.m_Success)
            {
                // disable UI to show
                CampaignID.Enabled = false;

                string mToken = string.Format(@"CC_{0}_{1}", GetCampaignID(), Tenants.Text);
                // Set Properties
                CampaignManagerSpace.CloseCampaignArgs mArgs = new CampaignManagerSpace.CloseCampaignArgs();
                mArgs.CampaignIdentifier = Convert.ToInt32(CampaignID.Text);
                mArgs.TenantDescriptor = Tenants.Text;
                mArgs.Token = mToken;

                EventHandler[mToken] = StopCampaignHandled;
                // Get the properties
                m_CampaignManager.CloseCampaign(mArgs);
            }
            else
            {
                MessageBox.Show(string.Format("Not able to grant lock on campaign id {0}",
                    CampaignID.Text) );
            }
        }

        private void StopCampaignHandled(EventObject pEvent)
        {
            // Campaign lock can be released regardless of success or fail
            UnlockTheCampaign();

            // Note - success means PD message will follow. No need to handle here
            if (!pEvent.m_Success)
            {
                MessageBox.Show(string.Format("Unable to stop campaign id {0}. Error {1} returned. Please check the Softdial Error lookup.",
                    CampaignID.Text,
                    pEvent.m_Error));
            }

        }
        private void StopCampaign_Click(object sender, EventArgs e)
        {
            string mToken = string.Format(@"LC_{0}_{1}", GetCampaignID(), Tenants.Text);

            EventHandler[mToken] = StopCampaignLocked;

            LockTheCampaign(GetCampaignID(), mToken);
        }
        #endregion StopCampaign
    }

    public class EventObject
    {
        public string m_Token;
        public bool m_Success;
        public int m_Error;

        public EventObject()
        {
            m_Success = false;
            m_Token = string.Empty;
            m_Error = 0;
        }
    }

}
