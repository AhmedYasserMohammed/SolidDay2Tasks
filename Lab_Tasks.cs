// The original interface had all methods combined which violates Interface Segregation Principle
public interface ILead
{
    void CreateSubTask();
    void AssignTask();
    void WorkOnTask();
}

// I have created new interfaces after applying ISP 
public interface ITaskCreator
{
    void CreateSubTask();
}

public interface ITaskAssigner
{
    void AssignTask();
}

public interface ITaskWorker
{
    void WorkOnTask();
}

// TeamLead implements all interfaces as it needs all capabilities
public class TeamLead : ITaskCreator, ITaskAssigner, ITaskWorker
{
    public void AssignTask()
    {
        Task t = new Task() { Title = "Merge and Deploy", Description = "Task to merge and deploy sharing feature to develop" };
        t.AssignTo(new Developer() { Name = "Developer1" });
    }

    public void CreateSubTask()
    {
        // Code to create a sub task
    }

    public void WorkOnTask()
    {
        // Code to work on task
    }
}

// Manager only needs to assign tasks and create subtasks, doesn't work on tasks
public class Manager : ITaskCreator, ITaskAssigner
{
    public void AssignTask()
    {
        // Manager-specific task assignment logic
    }

    public void CreateSubTask()
    {
        // Manager-specific subtask creation
    }
}

//===========================================
// 1 - Initial implementation
public interface ISqlFile
{
    string FilePath { get; set; }
    string FileText { get; set; }
    string LoadText();
    void SaveText();
}

public class SqlFile : ISqlFile
{
    public string FilePath { get; set; }
    public string FileText { get; set; }

    public string LoadText()
    {
        /* Code to read text from sql file */
        return FileText;
    }

    public void SaveText()
    {
        /* Code to save text into sql file */
    }
}

// 2 - After readonly requirement - Violates LSP (throws exception)
public class ReadOnlySqlFile : SqlFile
{
    public new void SaveText()
    {
        throw new IOException("Can't Save");
    }
}

// 3 - Proper refactored solution applying SOLID principles
public interface IReadableSqlFile
{
    string FilePath { get; set; }
    string FileText { get; set; }
    string LoadText();
}

public interface IWritableSqlFile : IReadableSqlFile
{
    void SaveText();
}

public class SqlFile : IWritableSqlFile
{
    public string FilePath { get; set; }
    public string FileText { get; set; }

    public string LoadText()
    {
        /* Code to read text from sql file */
        return FileText;
    }

    public void SaveText()
    {
        /* Code to save text into sql file */
    }
}

public class ReadOnlySqlFile : IReadableSqlFile
{
    public string FilePath { get; set; }
    public string FileText { get; set; }

    public string LoadText()
    {
        /* Code to read text from sql file */
        return FileText;
    }
}

public class SqlFileManager
{
    public List<IReadableSqlFile> AllSqlFiles { get; set; }
    public List<IWritableSqlFile> WritableSqlFiles { get; set; }

    public string GetTextFromFiles()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var file in AllSqlFiles)
        {
            sb.Append(file.LoadText());
        }
        return sb.ToString();
    }

    public void SaveTextIntoFiles()
    {
        foreach (var file in WritableSqlFiles)
        {
            file.SaveText();
        }
    }
}