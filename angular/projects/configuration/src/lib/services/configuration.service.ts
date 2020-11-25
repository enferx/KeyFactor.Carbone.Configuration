import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class ConfigurationService {
  apiName = 'Configuration';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/Configuration/sample' },
      { apiName: this.apiName }
    );
  }
}
