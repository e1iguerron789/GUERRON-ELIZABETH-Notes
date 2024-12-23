namespace Notes.Models;

    internal class EGNote
    {
      

        public string Filename { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public EGNote()
        {
            Filename = $"{Path.GetRandomFileName()}.notes.txt";
            Date = DateTime.Now;
            Text = "";
        }

        public void Save() =>
            File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename), Text);

        public void Delete() =>
            File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename));

        public static EGNote Load(string filename)
        {
            filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);

            return new()
            {
                Filename = Path.GetFileName(filename),
                Text = File.ReadAllText(filename),
                Date = File.GetLastWriteTime(filename)
            };
        }

        public static IEnumerable<EGNote> LoadAll()
        {
            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            return Directory
                .EnumerateFiles(appDataPath, "*.notes.txt")
                .Select(filename => EGNote.Load(Path.GetFileName(filename)))
                .OrderByDescending(note => note.Date);
        }
    }

