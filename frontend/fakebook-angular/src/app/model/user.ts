export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string | undefined;
  password: string;
  profilePictureUrl: string | null;
  status: string | undefined;
  birthDate: Date;
  followerIds: number[];
  followees: User[];
}
