import { useState } from "react"
import { UrlService } from "../api/UrlService";
import type { FormEvent } from 'react';

export default function UrlView() {
    const [inputUrl, setInputUrl] = useState('');
    const [shortUrl, setShortUrl] = useState('');
    const [error, setError] = useState('');

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            const data = await UrlService.shortenUrl(inputUrl);
            setShortUrl(data.shortUrl);
            setError('');
        } catch (error) {
            setError('Error');
        }
    };

    return (
        <div className="flex my-auto mx-auto justify-center items-center min-h-screen bg-blue-400">

            <div className="flex flex-row">
                <form onSubmit={handleSubmit}>
                    <input
                        type="text"
                        value={inputUrl}
                        onChange={(e) => setInputUrl(e.target.value)}
                        className="bg-white border rounded-none border-gray-800 shadow-sm p-2"
                        placeholder="Ingresa tu URL"
                    />
                    <button
                        type="submit"
                        className="border-none outline-1 -outline-offset-4 outline-dotted outline-black text-sm py-1.5 px-7 mx-2 bg-gray-400 cursor-pointer shadow-[inset_1px_1px_#fff,inset_-1px_-1px_#292929,inset_2px_2px_#ffffff,inset_-2px_-2px_rgb(158,158,158)] tracking-widest uppercase"


                    >Acortar</button>
                </form>
                {error && <p style={{ color: 'red' }}>{error}</p>}
                {shortUrl && <a href={shortUrl}>{shortUrl}</a>}
            </div>

        </div>
    );
}