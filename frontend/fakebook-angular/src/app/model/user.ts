export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string | undefined;
  password: string;
  profilePictureUrl: string | undefined;
  status: string | undefined;
  birthDate: Date;
  followers: User[];
  followees: User[];
}
