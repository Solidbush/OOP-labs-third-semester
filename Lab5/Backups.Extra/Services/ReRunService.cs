using Newtonsoft.Json;

namespace Backups.Extra.Services;

public class ReRunService
{
    private readonly JsonSerializerSettings _reRunSettings = new ()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        Formatting = Formatting.Indented,
    };
    private readonly string _reRunFilePath;

    public ReRunService(string reRunFilePath)
    {
        _reRunFilePath = reRunFilePath;
    }

    public void SetState(BackupTaskExtra taskExtra)
    {
        string data = JsonConvert.SerializeObject(taskExtra, _reRunSettings);
        File.WriteAllText(_reRunFilePath, data);
    }

    public BackupTaskExtra RestoreState()
    {
        return JsonConvert.DeserializeObject<BackupTaskExtra>(File.ReadAllText(_reRunFilePath), _reRunSettings);
    }
}