namespace MinyToDo.Models.Enums
{
    public enum PriorityType : byte
    {
        High = 0, // need to do now
        Medium = 1, // important but firstly need to do high priority
        Low = 2, // delegatable
        VeryLow = 3, // note or something like that
    }
}