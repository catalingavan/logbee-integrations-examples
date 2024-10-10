POST {{ApiUrl}}/request-logs
Content-Type: application/json
```
{
  "organizationId": "{{OrganizationId}}",
  "applicationId": "{{ApplicationId}}",
  "startedAt": "2024-10-09T15:54:17.488Z",
  "durationInMilliseconds": 41,
  "machineName": "PAYMENTS-SERVER",
  "sessionId": "e22131d4-5863-47a3-b33a-86d2be40f064",
  "isAuthenticated": true,
  "user": {
    "name": "user.name@example.com"
  },
  "httpProperties": {
    "absoluteUri": "https://payments-api/process-payment?attempt=1",
    "method": "POST",
    "request": {
      "headers": {
        "Content-Type": "application/x-www-form-urlencoded"
      },
      "formData": {
        "payment_token": "YmMzM2VlODktNWYyNi00MmM3LTk0YTMtZGI5NzVlZDkxZWUz"
      }
    },
    "response": {
      "statusCode": 500
    }
  },
  "logs": [
    {
      "logLevel": "Information",
      "message": "Payment has been initiated"
    }
  ],
  "exceptions": [
    {
      "exceptionType": "PaymentsApp.Processor.Exceptions.InsufficientBalance",
      "exceptionMessage": "Insufficient balance on payment"
    }
  ],
  "keywords": [
    "CorrelationId:bc33ee89-5f26-42c7-94a3-db975ed91ee3"
  ]
}
```
HTTP 200