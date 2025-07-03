import apiClient from "../config/axios";

export interface ShortenUrlResponse {
    key: string,
    longUrl: string,
    shortUrl: string
}

export const UrlService = {
    async shortenUrl(longUrl: string){
        try {
            const response = await apiClient.post('/', { 
                url: longUrl
            });
            return response.data;
        } catch (error) {
            console.error('Error:', error);
        }
    },

    async getUrl(key: string){
        try {
            const response = await apiClient.get(`/get/${key}`);
            return response.data;
        } catch (error) {
            console.error('Error:', error);
        }
    }
}