using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace tomware.Docson.Services
{
  public class MessageDefinition
  {
    public string Name { get; set; }
    public decimal Version { get; set; }
    public string Producer { get; set; }
    public object Message { get; set; }
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

    MessageDefinition GetByName(string name);

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

      // go an load them!
      await LoadDefintions(path);

      return this.Definitions.Values.OrderBy(o => o.Name);
    }

    public MessageDefinition GetByName(string name)
    {
      if (!this.Definitions.ContainsKey(name))
      {
        throw new InvalidDataException($"Type with name ${nameof(name)} does not exist!");
      }

      return this.Definitions[name];
    }

    public void Invalidate()
    {
      this.Definitions.Clear();
    }

    private async Task LoadDefintions(string path)
    {
      var files = Directory.GetFiles(path);
      foreach (var file in files)
      {
        if (file.EndsWith("schema.json")) continue;

        var type = await File.ReadAllTextAsync(file);
        var definition = JsonConvert.DeserializeObject<MessageDefinition>(type);
        this.Definitions.TryAdd(definition.Name, definition);
      }
    }
  }
}