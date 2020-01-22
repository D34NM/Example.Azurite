# An example of using Azurite for testing

This is a simple example of how to setup the Azurite together with Docker to enable testing without provisioning Azure resources. For now it only works with simple `Blob` storage, an idea would be to extend this to demonstrate all the feature Azurite provides.

## Dependencies

This project uses the following:

* Azurite  (https://github.com/Azure/Azurite)
* Docker   (https://docs.docker.com/compose/)

## The implementation

In the project `Example.Api` there are 2 configuration files:

* `appsettings.Test.json`
* `appsettings.Development.json`

that configure the following information for the Azure/Azurite:

```js
"Storage": {
    "ConnectionString": "",
    "Container": ""
  }
```

This is configured to the Azurite development connection string:

```js
"Storage": {
    "ConnectionString": "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://azurite:10000/devstoreaccount1;",
    "Container": "devstoreaccount1/test"
  }
```

The only difference is the endpoint on where to find the Azurite. There are two cases here: 

* if started `docker-compose up` the endpoint will be `http://azurite:10000/devstoreaccount1;`
* if you start Azurite using `docker run -p 10000:10000 mcr.microsoft.com/azure-storage/azurite azurite-blob --blobHost 0.0.0.0` then you need to configure the endpoint to your localhost (like in example, `http://127.0.0.1:10000`)

