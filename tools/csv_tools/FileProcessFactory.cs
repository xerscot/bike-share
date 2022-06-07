using System.Reflection;

namespace csv_tools
{
    public class FileProcessFactory 
    {
        Dictionary<string, Type>? processes;

        public FileProcessFactory()
        {
            GetReturnableTypes();
        }

        public IFileProcess CreateInstance(string process)
        {
            Type t = GetTypeToCreate(process);

            if(t == null)
                return new NullProcess();

            return Activator.CreateInstance(t) as IFileProcess;
        }

        Type GetTypeToCreate(string process)
        {
            foreach (var p in processes)
            {
                if (p.Key.Contains(process.ToLower()))
                {
                    return processes[p.Key];
                }
            }

            return null;
        }

        public void GetReturnableTypes()
        {
            processes = new Dictionary<string, Type>();

            Type[] typesInAssembly = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in typesInAssembly)
            {
                if (type.GetInterface(typeof(IFileProcess).ToString()) != null)
                {
                    processes.Add(type.Name.ToLower(), type);
                }
            }
        }
    }
}