using System.IO;
using Markdig;
using Markdig.Extensions.AutoIdentifiers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using tomware.Docson.Services;
using Westwind.AspNetCore.Markdown;

namespace tomware.Docson
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMarkdown(config =>
        {
          config.AddMarkdownProcessingFolder("/");

          // optional custom MarkdigPipeline (using MarkDig; for extension methods)
          config.ConfigureMarkdigPipeline = builder =>
          {
            builder
              .UseEmphasisExtras(Markdig.Extensions.EmphasisExtras.EmphasisExtraOptions.Default)
              .UsePipeTables()
              .UseGridTables()
              .UseAutoIdentifiers(AutoIdentifierOptions.GitHub) // Headers get id="name"
              .UseAutoLinks() // URLs are parsed into anchors
              .UseAbbreviations()
              .UseYamlFrontMatter()
              .UseEmojiAndSmiley(true)
              .UseListExtras()
              .UseFigures()
              .UseTaskLists()
              .UseCustomContainers()
              //.DisableHtml()   // renders HTML tags as text including script
              .UseGenericAttributes();
          };
        });

      services.AddDirectoryBrowser();

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      // Own services
      services.AddSingleton<IMessageDefinitionService, MessageDefinitionService>();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
      }

      app.UseStaticFiles();
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(
          Path.Combine(Directory.GetCurrentDirectory(), this.Configuration["DirectorySettings:TypesDirectory"])),
        RequestPath = this.Configuration["DirectorySettings:RequestPath"]
      });

      app.UseMarkdown();

      app.UseMvc();
    }
  }
}
