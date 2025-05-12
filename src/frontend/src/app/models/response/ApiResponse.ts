export class ApiResponse<T> {
    code: string;
    data: T;
    isSuccess: boolean;
    message?: string;
    timestamp: Date;
  
    constructor(code: string, data: T, isSuccess: boolean, message?: string) {
      this.code = code;
      this.data = data;
      this.isSuccess = isSuccess;
      this.message = message;
      this.timestamp = new Date();
    }
}