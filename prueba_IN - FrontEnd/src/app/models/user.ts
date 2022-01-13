export interface User {
    id: number;
    userName: string;
    password: string;
}

export interface LoginUserData {
    user: User;
    jwt: string;
}