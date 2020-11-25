import { ModuleWithProviders, NgModule } from '@angular/core';
import { MY_PROJECT_NAME_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class ConfigurationConfigModule {
  static forRoot(): ModuleWithProviders<ConfigurationConfigModule> {
    return {
      ngModule: ConfigurationConfigModule,
      providers: [MY_PROJECT_NAME_ROUTE_PROVIDERS],
    };
  }
}
