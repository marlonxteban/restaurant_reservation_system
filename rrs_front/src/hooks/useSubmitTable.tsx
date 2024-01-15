import { useState } from "react";

// define data types
type TableFormatData = {
    capacity: number,
    location: string,
};

// define hook
const useSubmitTable = () => {
    // define states
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<Error | null>(null);

    const submitTable = async (tableData: TableFormatData) => {
        setIsLoading(true);
        setError(null);

        try {
            const response = await fetch("https://localhost:44313/api/Tables", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    Capacity: tableData.capacity,
                    Location: tableData.location,
                }),
            });
            if (!response.ok) {
                throw new Error("Error: ${response.status} ${response.statusText}");
            }

            //success
            const data = await response.json();
            return data;
        }
        catch (error) {
            setError(error as Error);
        }
        finally {
            setIsLoading(false);
        }
    };

    return { isLoading, error, submitTable };
};

export type { TableFormatData };
export default useSubmitTable;