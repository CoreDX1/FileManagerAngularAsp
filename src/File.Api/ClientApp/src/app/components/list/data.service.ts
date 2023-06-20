import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { IFile } from "src/app/models/file/file";
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn: "root"
})
export class DataService {
    private urlApi = "http://localhost:5056/api/folder/view/camilo@gmail.com";
    constructor(private http: HttpClient) {}

    getAllFiles(path = ""): Observable<IFile> {
        const data = this.http.get<IFile>(`${this.urlApi}/${path}`);
        return data;
    }
}
