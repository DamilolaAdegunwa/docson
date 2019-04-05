using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace tomware.Docson.Services
{
  public class MessageDefinition
  {
    public string Key { get; set; }
    public string Name { get; set; }
    public decimal Version { get; set; }
    public string Producer { get; set; }
    public JObject Message { get; set; }
    public JObject Sample { get; set; }
    public List<string> Tags { get; set; }

    public string NormalizedTags
    {
      get
      {
        return string.Join(", ", this.Tags.OrderBy(o => o));
      }
    }
  }

  public interface IMessageDefinitionService
  {
    Task<IEnumerable<MessageDefinition>> GetTypes(string path);

    MessageDefinition GetByKey(string key);

    void Invalidate();
  }

  public class MessageDefinitionService : IMessageDefinitionService
  {
    private ConcurrentDictionary<string, MessageDefinition> Definitions { get; set; }

    public MessageDefinitionService()
    {
      Definitions = new ConcurrentDictionary<string, MessageDefinition>();
    }

    public async Task<IEnumerable<MessageDefinition>> GetTypes(string path)
    {
      if (this.Definitions.Count > 0)
      {
        return this.Definitions.Values.OrderBy(o => o.Name);
      }

      // go and load them!
      await LoadDefinitions(path);

      return this.Definitions.Values.OrderBy(o => o.Name);
    }

    public MessageDefinition GetByKey(string key)
    {
      if (!this.Definitions.ContainsKey(key))
      {
        throw new InvalidDataException($"Type with name ${nameof(key)} does not exist!");
      }

      return this.Definitions[key];
    }

    public void Invalidate()
    {
      this.Definitions.Clear();
    }

    private async Task LoadDefinitions(string path)
    {
      var files = Directory.GetFiles(path);
      foreach (var file in files)
      {
        if (file.EndsWith("schema.json")) continue;

        var type = await File.ReadAllTextAsync(file);
        var definition = JsonConvert.DeserializeObject<MessageDefinition>(type);
        this.Definitions.TryAdd(CreateKey(definition), definition);
      }
    }

    public static string CreateKey(MessageDefinition definition)
    {
      var key = $"{definition.Name}.{definition.Version.ToString()}";
      definition.Key = key;

      return key;
    }
  }
}