export const environment = {
  production: true,
  baseApiUrl: 'https://app-blackhole.azurewebsites.net/api/',
  msalConfig: {
    auth: {
        clientId: 'd3ff73c9-b303-4c84-bcb2-610d51003666',
        authority: 'https://login.microsoftonline.com/f54a929c-546d-4f54-8be7-68dacf23e3e0'
    }
  },
  apiConfig: {
      scopes: ['user.read'],
      uri: 'https://graph.microsoft.com/v1.0/me'
  }
};
