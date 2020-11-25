import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { ConfigurationComponent } from './components/configuration.component';
import { ConfigurationRoutingModule } from './configuration-routing.module';

@NgModule({
  declarations: [ConfigurationComponent],
  imports: [CoreModule, ThemeSharedModule, ConfigurationRoutingModule],
  exports: [ConfigurationComponent],
})
export class ConfigurationModule {
  static forChild(): ModuleWithProviders<ConfigurationModule> {
    return {
      ngModule: ConfigurationModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<ConfigurationModule> {
    return new LazyModuleFactory(ConfigurationModule.forChild());
  }
}
