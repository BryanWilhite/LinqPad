<Query Kind="Program">
  <Namespace>System.Net.Mail</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

class MailMessage
{
	public MailMessage (string from, string to,
		string subject = "",
		string body = "",
		MailPriority priority = MailPriority.Normal)
	{
		From = from;
		To = to;
		Subject = subject;
		Priority = priority;
	}
	
	public string From, To, Subject, Body, CC, Bcc, ReplyTo;
	public bool IsBodyHtml;
	public DeliveryNotificationOptions DeliveryNotificationOptions;
	public MailPriority Priority;
}

class SmtpClient
{
	public SmtpClient (string host, int? port = null)
	{
		Host = host;
		if (port != null) Port = port.Value;
	}

	public string Host;
	public int Port = 25;
	public ICredentialsByHost Credentials;
	public SmtpDeliveryMethod DeliveryMethod = SmtpDeliveryMethod.Network;
	public bool EnableSSL;
	public int TimeOut;
	
	public void Send (MailMessage mailMessage) { }
}

void Main()
{
	var mm = new MailMessage ("mail@albahari.com", "brian.madsen@live.com", body: "Hi, Brian!");
	new SmtpClient ("mail.iinet.net.au", 554).Send (mm);
}