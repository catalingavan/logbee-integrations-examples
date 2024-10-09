# REST

Requests can be created by consuming the REST endpoint provided by the logbee application:

## Create a Log Request

To create a new log request, use the following endpoint:

Values for __OrganizationId__ and __ApplicationId__ can be retrieved from the user-interface page of the application configuration.

```
POST https://api.logbee.net/request-logs
Content-Type: application/json
{
  "organizationId": "_OrganizationId_",
  "applicationId": "_ApplicationId_",
  "startedAt": "2024-10-09T15:54:17.488Z",
  "httpProperties": {
    "absoluteUri": "http://localhost/hello",
    "method": "GET",
    "response": {
      "statusCode": 200
    }
  },
  "logs": [
    {
      "logLevel": "Information",
      "message": "My first log message"
    }
  ]
}

Response:
200 OK
{
  "id": "a2f1300a-b786-49e0-a556-3e6a2440ac60",
  "organizationId": "0337cd29-a56e-42c1-a48a-e900f3116aa8",
  "applicationId": "4f729841-b103-460e-a87c-be6bd72f0cc9"
}
```

## Using Hurl for Testing

You can also interact with the Logbee API using [Hurl](https://hurl.dev/), a simple tool for making HTTP requests and performing assertions on HTTP responses.

```sh
hurl .\post_request_logs.hurl --variables-file ..\API_KEYS
```
