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
            if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length > MaxEntryLength)
            {
                ((Entry) sender).Text = e.OldTextValue ?? "";
            }
            else
            {
                EntryLength = ((Entry) sender).Text.Length;
            }
        }
    }
}