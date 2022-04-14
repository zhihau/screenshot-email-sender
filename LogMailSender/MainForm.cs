/*======================================================================================
 MODULE NAME: MainForm
 FILE NAME: MainForm.cs
 DATE: 2008/8/25
 VERSION: 1.2.4
 PURPOSE: Take screenshots as email attachments and send them at time intervals.
 AUTHEROR: Zhihau Shiu    REVIEWED by

 FUNCTIONS

 REFERENCES

 ATTENTION: 

 * $History: $
 MODIFICTION HISTORY:
 Version 1.0.0 - Date 2008/8/13 By Zhihau Shiu
 *               send screenshot of SimReport window to gmail
 Version 1.2.3 - Date 2008/8/15 By Zhihau Shiu
 *               Purpose change: send screenshot to gmail
 *               1. Fix bug: Another programm is processing the file.
 *                  Solution: MailMessage.Dispose();
 *               2. Fix bug: The program freezes.
 *                  Solution: Take screenshots and send email actions execute in a single thread.
 *               3. Fix bug:SmtpException: The SMTP server requires a secure connection or the client was not authenticated.
 *                  Solution: UseDefaultCredentials, Credentials, EnableSsl of SmtpClient settings are not correct.
 *               5. Add: Log function
 *               6. Add: clear log button   
 Version 1.2.4 - Date 2008/8/25 By Zhihau Shiu
 *               1. Add: Exception message
 *               2. Fix bug: Send mail error and capture the same screenshot every time.
 *                  Solution: On the first error, the image is locked. Cause the second screenshot image cannot be written. After fixing the bug, it takes a different screenshot every time even if the email is wrong.
 *               Comment:
 *                   if you already have made a connection and the server somehow dies , you will get following exception if you try to send data.
 *                  "An existing connection was forcibly closed by the remote host" 
 * ** This is the first versoin for creating to support the functions of
    current module, or the first revision based on the original file.
 [DEFECTS CORRECCTED/NEW FEATURES]

======================================================================================*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using Opensharp.Tool;
using LogMailSender.Properties;
using System.IO;
using System.Net.Mail;

namespace LogMailSender
{
    public partial class MainForm : Form
    {

        #region Private variable
        private const string VERSION = "1.2.4";
        private const string JPG_FILENAME = "Log.jpg";
        private string _logJpgFileName = "";
        private bool _someoneHere = false;
        private SmtpClient _smtpClient = null;
        private LogFile _outputPP=null ;
        private delegate void OnSaveThreadCallEventHandler(string txt); 
        private OnSaveThreadCallEventHandler OnSaveThreadCall;
        private Thread _thMain = null;
        private bool _done = false;
        private ProcessStartInfo _proc = null;
        static object locker = new object();
        private XMLIniFile _pXmlfile=null ;
        private const string _moduleName = "MainForm";
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
            InitialControlValues();
        }
        #endregion

        #region Pinvoke function
        [DllImport("user32")]
        private static extern IntPtr keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOZORDER = 0x0004;
        const UInt32 SWP_NOREDRAW = 0x0008;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        const UInt32 SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
        const UInt32 SWP_SHOWWINDOW = 0x0040;
        const UInt32 SWP_HIDEWINDOW = 0x0080;
        const UInt32 SWP_NOCOPYBITS = 0x0100;
        const UInt32 SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
        const UInt32 SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */

        const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
 
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindow",
SetLastError = true)]
        public static extern IntPtr GetNextWindow(
        IntPtr hwnd,
        [MarshalAs(UnmanagedType.U4)] int wFlag);

        ///summary>
        /// Virtual Messages
        /// </summary>
        public enum WMessages : int
        {
            WM_LBUTTONDOWN = 0x201, //Left mousebutton down
            WM_LBUTTONUP = 0x202,  //Left mousebutton up
            WM_LBUTTONDBLCLK = 0x203, //Left mousebutton doubleclick
            WM_RBUTTONDOWN = 0x204, //Right mousebutton down
            WM_RBUTTONUP = 0x205,   //Right mousebutton up
            WM_RBUTTONDBLCLK = 0x206, //Right mousebutton doubleclick
            WM_KEYDOWN = 0x100,  //Key down
            WM_KEYUP = 0x101,   //Key up
        }

        /// <summary>
        /// Virtual Keys
        /// </summary>
        public enum VKeys : int
        {
            VK_LBUTTON = 0x01,   //Left mouse button
            VK_RBUTTON = 0x02,   //Right mouse button
            VK_CANCEL = 0x03,   //Control-break processing
            VK_MBUTTON = 0x04,   //Middle mouse button (three-button mouse)
            VK_BACK = 0x08,   //BACKSPACE key
            VK_TAB = 0x09,   //TAB key
            VK_CLEAR = 0x0C,   //CLEAR key
            VK_RETURN = 0x0D,   //ENTER key
            VK_SHIFT = 0x10,   //SHIFT key
            VK_CONTROL = 0x11,   //CTRL key
            VK_MENU = 0x12,   //ALT key
            VK_PAUSE = 0x13,   //PAUSE key
            VK_CAPITAL = 0x14,   //CAPS LOCK key
            VK_ESCAPE = 0x1B,   //ESC key
            VK_SPACE = 0x20,   //SPACEBAR
            VK_PRIOR = 0x21,   //PAGE UP key
            VK_NEXT = 0x22,   //PAGE DOWN key
            VK_END = 0x23,   //END key
            VK_HOME = 0x24,   //HOME key
            VK_LEFT = 0x25,   //LEFT ARROW key
            VK_UP = 0x26,   //UP ARROW key
            VK_RIGHT = 0x27,   //RIGHT ARROW key
            VK_DOWN = 0x28,   //DOWN ARROW key
            VK_SELECT = 0x29,   //SELECT key
            VK_PRINT = 0x2A,   //PRINT key
            VK_EXECUTE = 0x2B,   //EXECUTE key
            VK_SNAPSHOT = 0x2C,   //PRINT SCREEN key
            VK_INSERT = 0x2D,   //INS key
            VK_DELETE = 0x2E,   //DEL key
            VK_HELP = 0x2F,   //HELP key
            VK_0 = 0x30,   //0 key
            VK_1 = 0x31,   //1 key
            VK_2 = 0x32,   //2 key
            VK_3 = 0x33,   //3 key
            VK_4 = 0x34,   //4 key
            VK_5 = 0x35,   //5 key
            VK_6 = 0x36,    //6 key
            VK_7 = 0x37,    //7 key
            VK_8 = 0x38,   //8 key
            VK_9 = 0x39,    //9 key
            VK_A = 0x41,   //A key
            VK_B = 0x42,   //B key
            VK_C = 0x43,   //C key
            VK_D = 0x44,   //D key
            VK_E = 0x45,   //E key
            VK_F = 0x46,   //F key
            VK_G = 0x47,   //G key
            VK_H = 0x48,   //H key
            VK_I = 0x49,    //I key
            VK_J = 0x4A,   //J key
            VK_K = 0x4B,   //K key
            VK_L = 0x4C,   //L key
            VK_M = 0x4D,   //M key
            VK_N = 0x4E,    //N key
            VK_O = 0x4F,   //O key
            VK_P = 0x50,    //P key
            VK_Q = 0x51,   //Q key
            VK_R = 0x52,   //R key
            VK_S = 0x53,   //S key
            VK_T = 0x54,   //T key
            VK_U = 0x55,   //U key
            VK_V = 0x56,   //V key
            VK_W = 0x57,   //W key
            VK_X = 0x58,   //X key
            VK_Y = 0x59,   //Y key
            VK_Z = 0x5A,    //Z key
            VK_NUMPAD0 = 0x60,   //Numeric keypad 0 key
            VK_NUMPAD1 = 0x61,   //Numeric keypad 1 key
            VK_NUMPAD2 = 0x62,   //Numeric keypad 2 key
            VK_NUMPAD3 = 0x63,   //Numeric keypad 3 key
            VK_NUMPAD4 = 0x64,   //Numeric keypad 4 key
            VK_NUMPAD5 = 0x65,   //Numeric keypad 5 key
            VK_NUMPAD6 = 0x66,   //Numeric keypad 6 key
            VK_NUMPAD7 = 0x67,   //Numeric keypad 7 key
            VK_NUMPAD8 = 0x68,   //Numeric keypad 8 key
            VK_NUMPAD9 = 0x69,   //Numeric keypad 9 key
            VK_SEPARATOR = 0x6C,   //Separator key
            VK_SUBTRACT = 0x6D,   //Subtract key
            VK_DECIMAL = 0x6E,   //Decimal key
            VK_DIVIDE = 0x6F,   //Divide key
            VK_F1 = 0x70,   //F1 key
            VK_F2 = 0x71,   //F2 key
            VK_F3 = 0x72,   //F3 key
            VK_F4 = 0x73,   //F4 key
            VK_F5 = 0x74,   //F5 key
            VK_F6 = 0x75,   //F6 key
            VK_F7 = 0x76,   //F7 key
            VK_F8 = 0x77,   //F8 key
            VK_F9 = 0x78,   //F9 key
            VK_F10 = 0x79,   //F10 key
            VK_F11 = 0x7A,   //F11 key
            VK_F12 = 0x7B,   //F12 key
            VK_SCROLL = 0x91,   //SCROLL LOCK key
            VK_LSHIFT = 0xA0,   //Left SHIFT key
            VK_RSHIFT = 0xA1,   //Right SHIFT key
            VK_LCONTROL = 0xA2,   //Left CONTROL key
            VK_RCONTROL = 0xA3,    //Right CONTROL key
            VK_LMENU = 0xA4,      //Left MENU key
            VK_RMENU = 0xA5,   //Right MENU key
            VK_PLAY = 0xFA,   //Play key
            VK_ZOOM = 0xFB, //Zoom key
        }

        #endregion

        #region Private function
        
        private void InitialControlValues()
        {
            _smtpClient = new SmtpClient(Settings.Default.GmailServer, Settings.Default.GmailServerPort);
            
            txtbUserName.Text = Settings.Default.UserName;
            txtbToAddress.Text = Settings.Default.ToAddress;
            txtbPassword.Text = Settings.Default.Password;
            nudHour.Value = Convert.ToDecimal(Settings.Default.SendEveryHour);
            nudMin.Value = Convert.ToDecimal(Settings.Default.SendEveryMin);
            nudSec.Value = Convert.ToDecimal(Settings.Default.SendEverySec);

            niTray.Text = this.Text + " - " + VERSION;
            _logJpgFileName = Application.StartupPath +"\\"+ JPG_FILENAME;
            OnSaveThreadCall = new OnSaveThreadCallEventHandler(SaveThreadCall);
            niTray.BalloonTipText = "Stop";
            niTray.BalloonTipTitle = this.Text + " - " + VERSION;
            _proc = new ProcessStartInfo(); // a process holder */

            _pXmlfile = new XMLIniFile(this.Text +".xml", true);
            _outputPP = new LogFile(_pXmlfile, "ProgramProcess", Application.StartupPath + "\\ProgramProcess\\");
        }

        public void Send(string gmailUserName, string gmailPassword, string toAddress, string subject, string messageBody, string fileName)
        {
            string functionName = "Send";
            MailMessage msg = new MailMessage();
            try
            {
                _smtpClient.EnableSsl = true;
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new System.Net.NetworkCredential(gmailUserName, gmailPassword);

                
                msg.To.Add(toAddress);
                msg.Subject = subject;

                LinkedResource logo = new LinkedResource(fileName, System.Net.Mime.MediaTypeNames.Image.Jpeg);
                logo.ContentId = "Logo";

                AlternateView av1 = AlternateView.CreateAlternateViewFromString(@"<html><body><h1></h1><img src=""cid:Logo""/></body></html>", null, System.Net.Mime.MediaTypeNames.Text.Html);
                //To refer to msg image in the html body, use <img src="cid: companylogo"/>
                av1.LinkedResources.Add(logo);

                msg.AlternateViews.Add(av1);
                msg.IsBodyHtml = true;

                if (gmailUserName.IndexOf('@') == -1)
                    msg.From = new MailAddress(gmailUserName + "@Gmail.com");
                else
                    msg.From = new MailAddress(gmailUserName);

                //object userState = msg;

                if (msg.From == null)
                {

                    if (gmailUserName.IndexOf('@') == -1)
                    {
                        msg.From = new MailAddress(gmailUserName + "@Gmail.com");
                    }
                    else
                    {
                        msg.From = new MailAddress(gmailUserName);
                    }
                }


                //_smtpClient.Send(msg);
                
                //Clean up
                msg.Dispose();
                
                Log("Send message subject:" + subject);
            }
            catch (Exception ex)
            {
                msg.Dispose();
                //TODO: Add error handling
                Log(_moduleName +"."+functionName +"("+ex.ToString () +")");
               // throw ex;
            }
        }

        public void Log(string txt)
        {
            lbLog.BeginInvoke(OnSaveThreadCall, new object[] { txt});
        }

        private void SaveThreadCall(string txt)
        {
            string[] c = { "\r\n" };
            string[] val = null;
            val = txt.Split(c, StringSplitOptions.RemoveEmptyEntries);
            string logLineTimeFormat = "yyyy/MM/dd HH:mm:ss| ";
            DateTime currentTime = DateTime.Now;
            String time = currentTime.ToString(logLineTimeFormat);

            for (int i = 0; i < val.Length; ++i)
            {
                lbLog.Items.Add(time + val[i]);
                _outputPP.log(0, val[i]);
                if (lbLog.Items.Count > 250)
                {
                    lbLog.Items.RemoveAt(0);
                }
            }

            int itemsPerPage = (int)(lbLog.Height / lbLog.ItemHeight);
            lbLog.TopIndex = lbLog.Items.Count - itemsPerPage;
        }

        private void SaveSettings()
        {
            Settings.Default.UserName = txtbUserName.Text;
            Settings.Default.Password = txtbPassword.Text;
            Settings.Default.ToAddress = txtbToAddress.Text;
            Settings.Default.SendEveryHour = nudHour.Value.ToString();
            Settings.Default.SendEveryMin = nudMin.Value.ToString();
            Settings.Default.SendEverySec = nudSec.Value.ToString();
            Settings.Default.Save();
        }

        private void EnableEditSetting()
        {
            gbGmailSetting.Enabled = true;
            gbTimer.Enabled = true;
        }

        private void DisableEditSetting()
        {
            gbTimer.Enabled = false;
            gbGmailSetting.Enabled = false;
        }

        #endregion

        #region GUI Event
        private void bRun_Click(object sender, EventArgs e)
        {
            if (bRun.Text.Equals("Stop"))
            {
                trTimer.Enabled = false;
                bRun.Text = "Run";
                EnableEditSetting();
                niTray.BalloonTipText = "Stop";
            }
            else
            {
                bRun.Text = "Stop";
                DisableEditSetting();
                niTray.BalloonTipText  = "Running...";
                trTimer.Interval = Convert.ToInt32(nudHour.Value) * 60 * 60 * 1000 + Convert.ToInt32(nudMin .Value ) * 60 * 1000 + Convert.ToInt32(nudSec.Value) * 1000;
                trTimer.Enabled = true;
                if (_thMain != null)
                {
                    _thMain.Abort();
                }
                _thMain = new Thread(new ThreadStart(StartTimer));
                _thMain.Start();  
            }
        }

        private void StartTimer()
        {
            DateTime startTime ;
            DateTime endTime ;
            lock (locker)
            {
                if (!_done)
                { 
                    startTime = DateTime .Now;
                    Log("start send" + startTime.ToString());
                    Process p = null;
                    _proc = new ProcessStartInfo(); // a process holder */
                    _proc.FileName = Application.StartupPath + "\\CmdCaptureWin.exe";
                    _proc.Arguments = "/f " + JPG_FILENAME + " /q 50";
                    _proc.UseShellExecute = false; /*do not show console for the process - a must*/
                    p = Process.Start(_proc);
                    p.WaitForExit(); /*wait indefinitely for the associated process to exit*/
                    p.Close();
                    //p.Dispose();
                    //p = null;
                    Send(txtbUserName .Text , txtbPassword .Text, txtbToAddress .Text , "SimReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"), "", _logJpgFileName);
                    endTime= DateTime.Now;
                    Log("end send" + endTime.ToString());
                    TimeSpan ts = endTime.Subtract(startTime);
                    Log("waste " + ts.Seconds + " secs"); 
                    _done = true; 
                }
            }
        }
        
        private void bAbout_Click(object sender, EventArgs e)
        {
            AboutForm frAbout = new AboutForm();
            frAbout.Text = "About " + this.Text;
            frAbout.labelProductName.Text = this.Text;
            frAbout.labelVersion.Text = "Version "+VERSION ;
            frAbout.labelCopyright.Text = "Copyright(C)2008";
            frAbout.labelProjectDirector.Text = "Project Director: Zhihau Shiu";
            frAbout.labelDeveloper.Text = "Developer: Zhihau Shiu";
            frAbout.labelCompanyName.Text = "Link: https://github.com/zhihau";

            if (frAbout.ShowDialog().Equals(DialogResult.OK))
            {
            }
        }

        private void trTimer_Tick(object sender, EventArgs e)
        {
            string functionName = "trTimer_Tick";
            if (_someoneHere)
            {
                return;
            }
            _someoneHere = true;
            try
            {
                if (_done)
                {
                    _done = false ; 
                    if (_thMain != null)
                    {
                        _thMain.Abort();
                    }
                    _thMain = new Thread(new ThreadStart(StartTimer ));
                    _thMain.Start();                   
                }
            }
            catch (Exception Ex)
            {
                Log(_moduleName +"."+functionName +"("+Ex.ToString () +")");
            }
            
            _someoneHere = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!trTimer.Enabled)
            {
                trTimer.Enabled = false;
                trTimer.Dispose();
                trTimer = null;
            }
            if (_thMain != null)
            {
                _thMain.Abort();
            }
            SaveSettings();
            base.OnClosing(e);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                niTray.Visible = true;
                niTray.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                niTray.Visible = false;
            }

        }

        private void niTray_Click(object sender, EventArgs e)
        {
            Show();
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }
        
        private void bClear_Click(object sender, EventArgs e)
        {
            lbLog.Items.Clear();
        }
        #endregion
    }
}
