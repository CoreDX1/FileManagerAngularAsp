export interface IMenu {
    name: string;
}

// export interface IFile {
//     isSuccess: boolean;
//     directory: string[];
//     files: string[];
// }

export interface IFile {
    success: boolean;
    message: string;
    data: Data;
}

export interface Data {
    path: string;
    directories: Directory[];
    files: any[];
    totalSize: number;
    lastModified: string;
    author: string;
    fileCount: number;
    directoryCount: number;
}

export interface Directory {
    name: string;
    path: string;
    size: number;
    createDate: string;
    userId: number;
}
