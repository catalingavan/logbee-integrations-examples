# Logbee API

Logbee provides API for logging events and requests from your applications.

## Create a Request Log

To create a new request log, use the following endpoint:

Values for \_OrganizationId\_ and \_ApplicationId\_ can be retrieved from the user-interface page of the application configuration.

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
  "organizationId": "_OrganizationId_",
  "applicationId": "_ApplicationId_"
}
```

## Using Hurl for Testing

You can also interact with the Logbee API using [Hurl](https://hurl.dev/), a simple tool for making HTTP requests and performing assertions on HTTP responses.

```sh
hurl .\hurl\post_request_logs.hurl --variables-file .\hurl\api_keys
```
