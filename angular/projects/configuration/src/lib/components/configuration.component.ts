import { Component, OnInit } from '@angular/core';
import { ConfigurationService } from '../services/configuration.service';

@Component({
  selector: 'lib-configuration',
  template: ` <p>configuration works!</p> `,
  styles: [],
})
export class ConfigurationComponent implements OnInit {
  constructor(private service: ConfigurationService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
