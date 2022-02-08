import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { RobotFormData, RobotResponse } from '../components/robot-form/robot-form.model';

@Injectable({
  providedIn: 'root'
})
export class RobotService {
  constructor(private http: HttpClient) {
  }

  getRobotFinalGridPosition(data: RobotFormData) : Observable<RobotResponse> {
    const queyParams = `sequence=${data.gridXBoundary} ${data.gridYBoundary}&`
      + data.sequences?.map(sequence => `sequence=${sequence.positions}&sequence=${sequence.instructions}`).join('&');

    const headers = { headers: { 'Access-Control-Allow-Origin': '*', 'Accept': '*/*', 'Content-Type' : 'application/x-www-form-urlencoded; charset=UTF-8' } };

    return this.http.get<RobotResponse>(`${environment.apiUrl}/MartianRobot?${queyParams}`, headers);
    // return this.http.get<RobotPositionResponse>(`${environment.apiUrl}/MartialRobot?${queyParams}`);
  }

}
