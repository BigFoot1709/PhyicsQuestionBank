using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace PhysicsQuestionBank
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.AllowDrop = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Byte[] result = (Byte[])new ImageConverter().ConvertTo(pictureBox1.Image, typeof(Byte[]));
            label1.Text = "" + result;

            var client = new MongoClient("mongodb+srv://BigFoot:Digger1709@cluster0.yn5ot.mongodb.net/DotaStatBotWeb?retryWrites=true&w=majority");

            var db = client.GetDatabase("QuestionBank1");

            var questionbank1 = db.GetCollection<BsonDocument>("QuestionBank1");

            var doc = new BsonDocument
            {
                {"name", "question123"},
                {"imagebin", result}
            };

            questionbank1.InsertOne(doc);

        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                var fileNames = data as string[];
                if (fileNames.Length > 0)
                    pictureBox1.Image = Image.FromFile(fileNames[0]);
            }
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                pictureBox1.DoDragDrop(pictureBox1.Image, DragDropEffects.Copy);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var client = new MongoClient("mongodb+srv://BigFoot:Digger1709@cluster0.yn5ot.mongodb.net/DotaStatBotWeb?retryWrites=true&w=majority");

            var db = client.GetDatabase("QuestionBank1");

            var questionbank1 = db.GetCollection<BsonDocument>("QuestionBank1");

            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("name", "question123");

            var docs = questionbank1.Find(filter).ToList();

            Console.WriteLine(docs[0][2]);

            byte[] buffer = docs[0][2].AsByteArray;

            pictureBox2.Image = byteArrayToImage(buffer);
        }

        public Image byteArrayToImage(byte[] bytesArr)
        {
            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }

    }

}
