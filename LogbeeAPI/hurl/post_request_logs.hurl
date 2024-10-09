POST {{ApiUrl}}/request-logs
Content-Type: application/json
```
{
  "organizationId": "{{OrganizationId}}",
  "applicationId": "{{ApplicationId}}",
  "startedAt": "2024-10-09T15:54:17.488Z",
  "durationInMilliseconds": 41,
  "httpProperties": {
    "absoluteUri": "http://localhost/catalog/createProduct?tokenId=20420&locale=en",
    "method": "POST",
    "request": {
      "headers": {
        "Content-Type": "application/x-www-form-urlencoded"
      },
      "formData": {
        "category_id": "100",
        "product_name": "DVD player"
      }
    },
    "response": {
      "statusCode": 302,
      "headers": {
        "Location": "/catalog/products?productId=208"
      }
    }
  },
  "logs": [
    {
      "logLevel": "Information",
      "message": "Creating product 'DVD player' ..."
    },
    {
      "logLevel": "Error",
      "message": "Domain.Exceptions.CategoryNotFoundException: Product category (id=100) does not exist"
    },
    {
      "logLevel": "Warning",
      "message": "Will attach the product to the default category"
    },
    {
      "logLevel": "Information",
      "message": "Product has been saved in database. ProductId: 208"
    }
  ],
  "keywords": [
    "product:208",
    "category:100"
  ]
}
```
HTTP 200