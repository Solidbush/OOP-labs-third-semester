namespace Backups.Exceptions;

public class CreateFileException : Exception
{
    public CreateFileException()
        : base("File with same name already exists in this directory!")
    {
    }

    public CreateFileException(string message)
        : base(message)
    {
    }
}