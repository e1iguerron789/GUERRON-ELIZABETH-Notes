using CommunityToolkit.Mvvm.Input;
using Notes.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Notes.ViewModels;

internal class EGNotesViewModel : IQueryAttributable
{
    public ObservableCollection<EGNoteViewModel> AllNotes { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectNoteCommand { get; }

    public EGNotesViewModel()
    {
        AllNotes = new ObservableCollection<EGNoteViewModel>(EGNote.LoadAll().Select(n => new EGNoteViewModel(n)));
        NewCommand = new AsyncRelayCommand(NewNoteAsync);
        SelectNoteCommand = new AsyncRelayCommand<EGNoteViewModel>(SelectNoteAsync);
    }

    private async Task NewNoteAsync()
    {
        if (Shell.Current != null)
        {
            await Shell.Current.GoToAsync(nameof(Views.NotePage));
        }
        else
        {
            // Manejar el caso donde Shell.Current es null
        }
    }

    private async Task SelectNoteAsync(EGNoteViewModel note)
    {
        if (note != null && Shell.Current != null)
        {
            await Shell.Current.GoToAsync($"{nameof(Views.NotePage)}?load={note.Identifier}");
        }
        else
        {
            // Manejar el caso donde note es null o Shell.Current es null
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            string noteId = query["deleted"].ToString();
            EGNoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            if (matchedNote != null)
                AllNotes.Remove(matchedNote);
        }
        else if (query.ContainsKey("saved"))
        {
            string noteId = query["saved"].ToString();
            EGNoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            if (matchedNote != null)
            {
                matchedNote.Reload();
                AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
            }
            else
            {
                AllNotes.Insert(0, new EGNoteViewModel(EGNote.Load(noteId)));
            }
        }
    }
}
