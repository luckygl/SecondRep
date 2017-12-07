using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
//this is Server

public class ServerManager : MonoBehaviour {

	Socket m_listenerSocket;
	Thread T1;

	// Use this for initialization
	void Start () {
		print ("ServerManager.Start()");
		IPAddress m_serverIp = IPAddress.Parse("192.168.1.113");
		IPEndPoint m_listenerPort = new IPEndPoint (m_serverIp,9990);
		m_listenerSocket = new Socket (AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
		m_listenerSocket.Bind (m_listenerPort);
		m_listenerSocket.Listen (666);
		T1 = new Thread (new ThreadStart(ThreadtoStartServer));
		T1.Start ();
	}

	void ThreadtoStartServer(){
		Debug.Log ("子线程开启");
		Socket clientSocket = m_listenerSocket.Accept ();		//这里要放到协程里吧，或者子线程
		print ("监听到连接请求");
		string message = "hello,I'm Unity TCP Server";
		byte[] sendbyteArr = System.Text.Encoding.UTF8.GetBytes (message);
		int successSendBytes = clientSocket.Send (sendbyteArr,sendbyteArr.Length,SocketFlags.None);

		while (true) {
			byte[] receivebytes = new byte[1024];
			int successReceiveBytes = clientSocket.Receive (receivebytes);
			string str=System.Text.Encoding.UTF8.GetString(receivebytes);
			print ("接收到客户端信息："+str);
			int successSendBytes2 = clientSocket.Send (receivebytes,receivebytes.Length,SocketFlags.None);
		}

	}


}
