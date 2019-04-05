## Idea
This little site serves as a documentation for your system wide **Integration Event Message** types.

It aims to be **the place** for providing information (documenting event massages) about your 
system wide used "Integration Event Message" types.

It provides the following features:
- Markdown enabled index page for providing purpose and documentation.
- Download of json schema for creating and validation your message definition while developing.
- Browse definitions
  - filter by version
  - filter by producer
  - filter by tags


## Usage
1. First go and **[download](./types/schema.json)** the definition schema.
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