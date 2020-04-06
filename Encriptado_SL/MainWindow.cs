/*

Created by:
 ____  _        ____  __       ____          _         __  
/ ___|| |      / /  \/  |_ __ / ___|___   __| | ___ _ _\ \ 
\___ \| |     | || |\/| | '__| |   / _ \ / _` |/ _ \ '__| |
 ___) | |___  | || |  | | | _| |__| (_) | (_| |  __/ |  | |
|____/|_____| | ||_|  |_|_|(_)\____\___/ \__,_|\___|_|  | |
               \_\                                     /_/ 


*/
using System;
using Gtk;
using System.IO;
public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel) => Build();

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    //Save button function
    protected void guardar(object sender, EventArgs e)
    {
        string key = entry4.Text; //Gets key from user input
        if(entry4.Text != "" && entry4.Text.Length == 32) //If the entry is a valid key this happens
        {
            string path1 = entry1.Text;
            string filename = entry3.Text;
            string abc=" abcdefghijklmnopqrstuvwxyz,.-!?";
            using(StreamWriter file1 = new StreamWriter(path1 + "/" + filename))
            {
                string texto = textview1.Buffer.Text.ToLower(); //Get the text and make it lower case
                string finalfile = "";
                //Changes normal letters with the letters in the key
                for(int i=0;i < texto.Length; i++)
                {
                    finalfile = finalfile + entry4.Text[abc.IndexOf(texto[i])];
                }
                //Saves the encripted text into a file
                file1.WriteLine(finalfile);
                file1.Dispose();
                file1.Close();
            }
        }
        else if(key.Length != 32 && key.Length != 0) //if the key si not empty but is not 32 charactereither this happens
        {
            textview1.Buffer.Text = "{ERROR: KEY IS DOESN'T HAVE 32 CHARACTERS, IT HAS " + entry4.Text.Length.ToString() + "}" + textview1.Buffer.Text;
            /*Gtk.MessageDialog md = new Gtk.MessageDialog (null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "{ERROR: THE KEY LENGTH IS NOT 32 }");
            md.Run ();
            md.Destroy ();*/
        }
        else  //If the key is empty this happens
        {
            string path1 = entry1.Text;
            string filename = entry3.Text;
            using(StreamWriter file1 = new StreamWriter(path1 + "/" + filename))
            {
                //Saves the text into a normal file
                string texto = textview1.Buffer.Text;
                file1.WriteLine(texto);
                file1.Dispose();
                file1.Close();
            }
        }
    }
   
    protected void cargar(object sender, EventArgs e)
    {
        string path1 = entry1.Text;
        string filename = entry3.Text;
        if(File.Exists(path1+"/" +filename)) //Detect if the file exists or not
            { 
                string key = entry4.Text;
                 if(key != "" && key.Length == 32) //If the key is valid
                    { 
                        string abc=" abcdefghijklmnopqrstuvwxyz,.-!?";
                        using(StreamReader reader = new StreamReader(path1 + "/" + filename))
                        { 
                            string texto = reader.ReadToEnd().ToLower(); //Get the text of the file and makes it lowercase
                            string finalfile = "";
                            for(int i=0;i < texto.Length - 1; i++) //Changes the letters of the key to normal letters (unencripts)
                            {
                                finalfile = finalfile + abc[key.IndexOf(texto[i])];
                            }
                            textview1.Buffer.Text = finalfile; //Displays the text into the gui replacing whatever there is on the textview
                        }
                    }
                   else if(key.Length != 32 && key.Length != 0) //if the key is invalid and it isn't empty
                     { //Prints an error in the gui
                        textview1.Buffer.Text = "{ERROR: KEY IS DOESN'T HAVE 32 CHARACTERS, IT HAS " + entry4.Text.Length.ToString() + "}" + textview1.Buffer.Text;
                      }
                    else  //If key is empty
                    {   
                    using(StreamReader file1 = new StreamReader(path1 + "/" + filename)) // Loads the file as a normal text file
                     {
                        string texto = textview1.Buffer.Text;
                        textview1.Buffer.Text = texto + " " + file1.ReadToEnd();
                        file1.Dispose();
                        file1.Close();
                      }
                    }
             }
        else //If file doest exist this happens
        {
            textview1.Buffer.Text = textview1.Buffer.Text + "    {ERROR NO SUCH FILE}"; //Print an error in the gui
        }
    }
}
