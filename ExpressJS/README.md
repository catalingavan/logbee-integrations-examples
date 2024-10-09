# ExpressJS

ExpressJS application using [@logbee/express](https://www.npmjs.com/package/@logbee/express) package to send the logs to Logbee.

To get started, run:

```
npm i
node .\index.js
```

```js
const express = require('express');
const { logbee } = require('@logbee/express');

const app = express();
const port = 3000;

app.use(express.json());
app.use(express.urlencoded({ extended: true }));

app.use(logbee.middleware({
    organizationId: '_OrganizationId_',
    applicationId: '_ApplicationId_',
    logbeeApiUri: 'https://api.logbee.net'
}));

app.get("/", (req, res) => {
    const logger = logbee.logger(req);
    logger?.info('Hello world from ExpressJS');

    res.send('Hello World!');
});

app.use(logbee.exceptionMiddleware());

app.listen(port, () => {
    console.log(`[server]: Server is running at http://localhost:${port}`);
});
```
