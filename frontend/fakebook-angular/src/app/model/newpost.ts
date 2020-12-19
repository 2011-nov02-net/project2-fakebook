import { User } from './user';

export class newPost {
    constructor(
    public content: string,
    public user: User | undefined,
    public pictureUrl: string | undefined
    )
    {}
}
