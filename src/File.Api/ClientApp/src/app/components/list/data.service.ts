import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { IFile } from "src/app/models/file/file";
import { HttpClient } from "@angular/common/http";
import { folderCreateRequestDto } from "src/app/models/Folder/folderCreateRequestDto";
import { folderResponseDto } from "src/app/models/Folder/folderResponseDto";

@Injectable({
    providedIn: "root"
})
export class DataService {
    private urlApi =
        "http://localhost:5056/api/folder/view/146f2da060d3470db7cc1ab157fce061";
    constructor(private http: HttpClient) {}

    getAllFiles(path = ""): Observable<IFile> {
        const data = this.http.get<IFile>(`${this.urlApi}/${path}`);
        return data;
    }

    postCreateFolder(
        folder: folderCreateRequestDto
    ): Observable<folderResponseDto> {
        const response = this.http.post<folderResponseDto>(
            `${this.urlApi}`,
            folder
        );
        return response;
    }
}
