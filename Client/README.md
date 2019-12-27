JavaScript client for AppForm.HubController for ASP.NET Core

## Installation

```bash
npm install @appformllc/hubcontroller
```

## Usage

See the [AppForm.HubController Documentation](https://github.com/appform-consulting-llc/AppForm.HubController).

### Browser

To use the client in a browser, copy `*.js` files from the `dist/browser` folder to your script folder include on your page using the `<script>` tag. This should be added after including the base `@aspnet/signalr` browser script.

### Webpack

To use the client in a NodeJS application, install the package to your `node_modules` folder and use `import '@appformllc/hubcontroller'` to load the module. This should be called after calling `import { HubConnectionBuilder } from '@aspnet/signalr'` to include the base client.

### Example (Browser)

```JavaScript
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

...

connection.execute('demo/getNames')
  .then(result => {
    console.log('success!', result);
  })
  .catch(error => {
    console.error('uh oh!', error);
  });

connection.execute('demo/findName', 'John Doe')
  .then(result => {
    console.log('success!', result);
  })
  .catch(error => {
    console.error('uh oh!', error);
  });
```

### Example (Webpack)

```JavaScript
import { HubConnectionBuilder } from '@aspnet/signalr';
import '@appformllc/hubcontroller'

let connection = new HubConnectionBuilder()
    .withUrl("/chat")
    .build();

...

connection.execute('demo/getNames')
  .then(result => {
    console.log('success!', result);
  })
  .catch(error => {
    console.error('uh oh!', error);
  });

connection.execute('demo/findName', 'John Doe')
  .then(result => {
    console.log('success!', result);
  })
  .catch(error => {
    console.error('uh oh!', error);
  });
```
