using System;
using System.Linq;
using System.Windows;

namespace LevelEditor
{
    public static class DatabaseHelper
    {
        public static void ExportToDatabase()
        {
            LevelEditorDatabaseDataContext db = new LevelEditorDatabaseDataContext();

            try
            {
                IOrderedQueryable<ImagePath> toDelete =
                    (from a in db.ImagePaths orderby a.Id select a);
                db.ImagePaths.DeleteAllOnSubmit(toDelete);

                db.SubmitChanges();

                //Restarts the Id numbering so that it will start at 1 instead of 180++
                db.ExecuteCommand("DBCC CHECKIDENT('dbo.ImagePaths', RESEED, 0);");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            finally
            {
                string path = "../../Sprites/Tiles/";

                foreach (string f in System.IO.Directory.GetFiles(path))
                {
                    //string filename = f.Substring(path.Length - 1, f.Length - 4);
                    string filename = f.Substring(path.Length, f.Length - 4 - path.Length);
                    //-4 to remove file extension
                    string description = splitWord(filename);

                    db.ImagePaths.InsertOnSubmit(
                        new ImagePath()
                        {
                            Path = f.Substring(6),
                            Description = description
                        }
                    );
                }
                db.SubmitChanges();
            }
            db.Connection.Close();
        }
        private static string splitWord(string s)
        {
            string desc = "";
            desc += Char.ToUpper(s[0]);
            int i = 1;
            foreach (Char c in s.Substring(i, s.Length - i))
            {
                if (Char.IsUpper(c))
                {
                    desc += " " + splitWord(s.Substring(i));
                    break;
                }
                else if (c == '_')
                {
                    desc += " " + splitWord(s.Substring(i + 1));
                    break;
                }
                else if (Char.IsDigit(c))
                {
                    desc += " " + c;
                }
                else
                {
                    desc += c;
                }
                i++;
            }
            return desc;
        }
    }
}
