export interface folderResponseDto {
    success: boolean;
    message: string;
    data: Data;
}

interface Data {
    id: number;
    name: string;
    path: string;
    size: number;
    createDate: string;
    userId: number;
    isDeleted: boolean;
    deletedDate: any;
    files: any[];
    user: string;
}
