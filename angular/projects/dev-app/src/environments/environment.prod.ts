import { Config } from '@abp/ng.core';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'Configuration',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44372',
    clientId: 'Configuration_ConsoleTestApp',
    dummyClientSecret: '1q2w3e*',
    scope: 'Configuration',
    oidc: false,
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44372',
      rootNamespace: 'KeyFactor.Carbone.Configuration',
    },
    Configuration: {
      url: 'https://localhost:44391',
      rootNamespace: 'KeyFactor.Carbone.Configuration',
    },
  },
} as Config.Environment;
