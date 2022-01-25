using Xamarin.Forms;

namespace Wasted.Utils
{
    public class EntryLengthValidator : ChangeablePropertyObject
    {
        public int MaxEntryLength { get; set; }
        private int _entryLength;

        public int EntryLength
        {
            get => _entryLength;
            set
            {
                _entryLength = value;
                OnPropertyChanged();
            }
        }

        public EntryLengthValidator(int maxEntryLength)
        {
            MaxEntryLength = maxEntryLength;
        }

        public void EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry) sender;
            if (e.NewTextValue?.Length > MaxEntryLength)
            {
                entry.Text = e.OldTextValue ?? "";
            }
            else
            {
                EntryLength = entry.Text.Length;
            }
        }
    }
}