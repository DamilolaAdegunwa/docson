# Docson - a way to document your integration message types

During my day to day job I started like many other developers to apply  the "microservice pattern" or whatever you want to call it. Fact is, that those different applications that build your system need to talk to each other either over HTTP or over a kind of Message Broker. 

Speaking of the latter, you start to struggle sooner or later with all the message types which exists. What is their meaning? Which one to consume? Who is the publisher and who is the consumer?

Those question came up quite often the last time within our team. We felt that it is time to stick to some kind of documentation of how to build and use our "Integration Messages". 

That's what this post is all about and where `Docson` comes in.

## Idea
`Docson` is a small [ASP.NET Razor Page](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-2.2&tabs=visual-studio) site which serves as a documentation for your system wide **Integration Event Message** types.

It aims to be **the place** for providing information about your system wide used "Integration Event Message" types.

It provides the following features:
- Markdown enabled index page for providing purpose and documentation.
- Download of JSON schema for creating and validation your message definition while developing.
- Browse definitions:
  - filter by version
  - filter by producer
  - filter by tags

### ==> Animated Gif <==

## Process of creating a new **Integration Event Message**
1. First go and download the definition schema that you defined within your team (can be provided within `Docson`).
2. Create your definition type based on your requirements.
3. Validate your integration message type with downloaded schema (see [schema.net](https://json-schema.org/)).
4. Once it is valid store your definition type as an asset under `wwwroot/data/types` or the appropriate docker volume (i.e. /docson/data/types on a shared drive) if using the docker image.
5. That's it! It should now be browsable within `Docson`.


## Sample
The following is a sample of a `SampleMessage` integration event message.

```json
{
  "$schema": "./schema.json",
  "name": "SampleMessage",
  "version": 1.0,
  "producer": "DeviceService",
  "message": {
    "properties": {
      "id": {
        "type": "string"
      },
      "timestamp": {
        "type": "date"
      },
      "deviceIdent": {
        "type": "string"
      },
      "configuration": {
        "type": "string"
      }
    }
  },
  "tags": [
    "DeviceService"
  ]
}
```

#### Instance of a `SampleMessage` type
```json
{
  "id": "SomeId-Guid-WhatElse",
  "timestamp": "2012-04-21T18:25:43-05:00",
  "deviceIdent": "SomeDeviceIdentLike5",
  "configuration": "SomeSerializedJsonString"
}
```

## Summary
While creating `Docson` I took the chance and got myself a bit into ASP.NET Razor Pages. It is a perfect fit in my opinion. There is also a ready to go [docker image](https://hub.docker.com/r/tomware/microwf-playground) up on [docker hub](https://hub.docker.com) for `Docson`. You can find the [source code](https://github.com/thomasduft/microwf) up on [github.com](https://github.com) and if interested do PR's ;-).

In terms of styling and the general look and feel for the application there is still some room for improvments. The same goes for some functionalities like i.e. Upload  or update a message definition with via a upload form. But the core bits are there and it already does its job.

Greets Thomas