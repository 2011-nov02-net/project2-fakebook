import { User } from './user';

export class newPost {
    constructor(
    public content: string,
    public userId: string | undefined,
    public pictureUrl: string | undefined
    )
    {}
}
