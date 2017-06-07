﻿using System;
using System.Windows.Forms;

namespace GobangClient
{
    public partial class GameForm : Form
    {
        public static TcpHelperClient thc;
        public GameForm()
        {
            InitializeComponent();
            ControlHander.Init(rtxtRoom, rtxtState);//这个初始化方法必须放在实例化TcpHelperServer的前面，否则实例化后Reader线程启动，而初始化还没有进行，程序崩溃
            GameBoard.main.Init(pbChessBoard);
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            string[] messages = rtxtInput.Lines;string now;
            rtxtInput.Clear();
            for (int i = 0; i < messages.Length; i++)
            {
                now = messages[i];
                thc.Writer(now);
                now = TcpHelperClient.NickName + ":[" + DateTime.Now.ToString("HH:mm:ss") + "]\r\n" + now;
                ControlHander.Write(1, now);
            }
        }

        private void pbChessBoard_MouseClick(object sender, MouseEventArgs e)
        {
            if (GameBoard.is_playing)
            {
                GameBoard.main.Print(e, GameBoard.mycolor);
            }
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            thc = TcpHelperClient.main;
            thc.ThreadWakeUp();
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
