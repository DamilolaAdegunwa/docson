# Docson - a way to document your integration message types

During my day to day job I started like many other developers to apply  the "microservice pattern" or whatever you want to call it. Fact is, that those different applications that build your system need to talk to each other either over HTTP or over a kind of Message Broker. 

Speaking of the latter, you start to struggle sooner or later with all the message types which exists. What is its meaning? Which one to consume? Who is the publisher, who is the producer and who is the consumer? 

Those question came up quite often the last time within our team. We felt that it is time to stick to some kind of documentation of how to build and use our "Integration Messages". 

That's what this post is all about and where `Docson` comes in.

## Idea
Docson is a small [ASP.NET Razor Page](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-2.2&tabs=visual-studio) site which serves as a documentation for your system wide **Integration Event Message** types.

It aims to be **the place** for providing information about your system wide used "Integration Event Message" types.

It provides the following features:
- Markdown enabled index page for providing purpose and documentation.
- Download of JSON schema for creating and validation your message definition while developing.
- Browse definitions
  - filter by version
  - filter by producer
  - filter by tags


## Usage
1. First go an **[download](./types/schema.json)** the definition schema.
2. Create your definition type.
3. Validate your integration message type (link to json schema.net).
4. Once it is valid store your definition type as an asset under **wwwroot/data/types**.
5. That's it! Head over to the [Definitions](./Definitions).


## Sample
The following is a sample of a `SampleMessage` integration event message.

#### Definition of the `SampleMessage` type
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