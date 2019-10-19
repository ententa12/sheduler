namespace CSVEmailModel
{
    public class EmailPerson
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        override public string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Email)}: {Email}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Title)}: {Title}, {nameof(Message)}: {Message}";
        }
    }
}