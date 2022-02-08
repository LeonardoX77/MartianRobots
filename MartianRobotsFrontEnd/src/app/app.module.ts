import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { GridComponent } from './components/grid/grid.component';
import { RobotModule } from './components/robot/robot.module';
import { GridCellComponent } from './components/grid/grid-cell/grid-cell.component';
import { RobotFormModule } from './components/robot-form/robot-form.module';
import { MainComponent } from './components/main/main.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    GridComponent,
    GridCellComponent,
    MainComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    RobotModule,
    RobotFormModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
