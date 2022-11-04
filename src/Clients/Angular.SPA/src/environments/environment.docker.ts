export const environment = {
  production: false,
  authApi: 'http://host.docker.internal:5000/',
  backApi: 'http://host.docker.internal:5100/',
  signalrApi: 'http://host.docker.internal:5400/',
  tokenWhiteListedDomains: ['host.docker.internal:5000', 'host.docker.internal:5100']
};
