using System.Net;
using System.Net.Mail;

namespace MyMail
{
    public partial class Form1 : Form
    {
        static string smtp;
        static int port;
        static string email;
        static string password;
        static string subject;
        static string message;
        static List<string> emails=new List<string>();
        static List<string> attachments = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            smtp = textBox2.Text;
            port = Convert.ToInt32(numericUpDown1.Value);
            email = textBox3.Text;
            password = textBox4.Text;
            subject = textBox5.Text;
            message = textBox6.Text;
            foreach (string item in listBox1.Items)
            {
                emails.Add(item);
            }




            if (smtp== "" || port==0 || email=="" || password==""|| listBox1.Items.Count==0  )
            {
                MessageBox.Show("Error!");
            }else if(subject == "" || message == "")
            {
                MessageBox.Show("Enter subject and your message!");

            }
            else
            {

                SmtpClient smtpClient = new SmtpClient(smtp, port);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(email, password);
                smtpClient.Send(GetMailMessageWithAttachment());


                MessageBox.Show("Message sent!");
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
            textBox1.Text = "";
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private  MailMessage GetMailMessageWithAttachment()
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(email);
            for(int i=0;i<emails.Count;i++)
            {
                mailMessage.To.Add(emails[i]);

            }
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = false;
            mailMessage.Body = message;

            foreach (string item in listBox2.Items)
            {
                Attachment attachment = new Attachment(item);//file path
                mailMessage.Attachments.Add(attachment);
            }
            
            return mailMessage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(textBox7.Text);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            listBox2.Items.Remove(listBox2.SelectedItem);
        }
    }
}